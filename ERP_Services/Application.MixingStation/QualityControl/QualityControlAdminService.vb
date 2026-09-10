'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Duván Albeiro Mejia Cortes 
' Created          : 2021-10-05
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Transactions
Imports Application.Inventory
Imports Application.Inventory.TransferOrder
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources

Public Class QualityControlAdminService
    Implements IQualityControlAdminService, Inject

    ''' <summary>
    ''' repository
    ''' </summary>
    Private ReadOnly _requestPackageDetailStatusRepository As IRequestPackageDetailStatusRepository
    Private ReadOnly _transferOrderAdminService As ITransferOrderAdminService
    Private ReadOnly _requestPackageDetailStatusDefectClassificationRepository As IRequestPackageDetailStatusDefectClassificationRepository
    Private ReadOnly _requestPackageDetailStatusDefectClassificationDetailRepository As IRequestPackageDetailStatusDefectClassificationDetailRepository
    Private ReadOnly _campaignReportsAdminService As ICampaignReportsAdminService
    Private ReadOnly _requestMixingStationRepository As IRequestMixingStationRepository
    Private ReadOnly _thirdPartyRepository As IThirdPartyRepository
    Private ReadOnly _causeReprocessingRejectionRepository As ICauseReprocessingRejectionRepository
    Private ReadOnly _mixingStationSettingRepository As IMixingStationSettingRepository
    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenseDetailRepository As IMixingStationSequenceDetailRepository


    ''' <summary>
    ''' Constructor 
    ''' </summary>
    ''' <param name="RequestPackageDetailStatusRepository"></param>
    Public Sub New(RequestPackageDetailStatusRepository As IRequestPackageDetailStatusRepository,
                   transferOrderAdminService As ITransferOrderAdminService,
                   requestPackageDetailStatusDefectClassificationRepository As IRequestPackageDetailStatusDefectClassificationRepository,
                   requestPackageDetailStatusDefectClassificationDetailRepository As IRequestPackageDetailStatusDefectClassificationDetailRepository,
                   campaignReportsAdminService As ICampaignReportsAdminService,
                   requestMixingStationRepository As IRequestMixingStationRepository,
                   mixingStationSettingRepository As IMixingStationSettingRepository,
                   causeReprocessingRejectionRepository As ICauseReprocessingRejectionRepository,
                   secuenseDetailRepository As IMixingStationSequenceDetailRepository,
                   thirdPartyRepository As IThirdPartyRepository)
        _thirdPartyRepository = thirdPartyRepository
        _secuenseDetailRepository = secuenseDetailRepository
        _transferOrderAdminService = transferOrderAdminService
        _campaignReportsAdminService = campaignReportsAdminService
        _requestMixingStationRepository = requestMixingStationRepository
        _mixingStationSettingRepository = mixingStationSettingRepository
        _requestPackageDetailStatusRepository = RequestPackageDetailStatusRepository
        _causeReprocessingRejectionRepository = causeReprocessingRejectionRepository
        _requestPackageDetailStatusDefectClassificationRepository = requestPackageDetailStatusDefectClassificationRepository
        _requestPackageDetailStatusDefectClassificationDetailRepository = requestPackageDetailStatusDefectClassificationDetailRepository
    End Sub

#Region "Properties"
    Private Const FORM_NAME As String = "FrmDashboardQualityControl"
#End Region

#Region "Functions"
    ''' <summary>
    ''' Lista de chequeo de la clasificacion de defectos por detail status
    ''' </summary>
    ''' <param name="requestPackageDetailStatusIds"></param>
    ''' <returns></returns>
    Public Function GetRequestPackageDetailStatusDefectClassification(requestPackageDetailStatusIds As List(Of Integer), unitDoseClass As Integer, Optional Form As Byte = 0) As List(Of DefectClassificationModel) Implements IQualityControlAdminService.GetRequestPackageDetailStatusDefectClassification
        Try
            If requestPackageDetailStatusIds.Any() Then
                Dim requestPackageDetailCount = _requestPackageDetailStatusRepository.Count(Function(m) requestPackageDetailStatusIds.Contains(m.Id))
                If requestPackageDetailCount = 0 Then Return Nothing
            End If

            Dim rpd = _requestPackageDetailStatusRepository.GetRequestPackageDetailStatusDefectClassification(requestPackageDetailStatusIds, unitDoseClass, Form)
            Return rpd

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Guarda los datos del checklist
    ''' </summary>
    ''' <param name="requestPackageDetailStatusIds"></param>
    ''' <param name="DefectClassificationHeader"></param>
    ''' <param name="lst"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveDefectClassificationByRequestPackageDetailStatus(
    isQuality As Boolean,
    requestPackageDetailStatusIds As List(Of Integer),
    DefectClassificationHeader As DefectClassificationHeaderModel,
    lst As List(Of DefectClassificationModel),
    audit As AuditMessage,
    Optional ForceSave As Boolean = False) As ActionResult Implements IQualityControlAdminService.SaveDefectClassificationByRequestPackageDetailStatus

        Try
            If Not lst.Any() Then
                Throw New IndigoValidationException("No se ha enviado datos")
            End If

            Dim _validations As New List(Of String)

            Using scope As New TransactionScope(TransactionScopeOption.Required,
                                            New TransactionOptions With {
                                                .Timeout = TransactionManager.MaximumTimeout,
                                                .IsolationLevel = IsolationLevel.ReadCommitted
                                            })

                ' Validación de tipo de dosis
                Dim IdBase As Integer = requestPackageDetailStatusIds(0)
                Dim unitDoseType = _requestPackageDetailStatusRepository _
                .FirstOrDefault(Function(m) m.Id = IdBase,
                                includes:={"RequestMixingStationDetail.UnitDoseType"}) _
                ?.RequestMixingStationDetail?.UnitDoseType?.MSClass

                If Not {EUnitDoseTypeClass.Repackaging, EUnitDoseTypeClass.Refilling}.Contains(If(unitDoseType, 0)) Then

                    For Each requestPackageDetailStatusId In requestPackageDetailStatusIds
                        Dim oldData = _requestPackageDetailStatusDefectClassificationRepository _
                        .FirstOrDefault(Function(m) m.RequestPackageDetailStatusId = requestPackageDetailStatusId,
                            includes:={
                                "RequestPackageDetailStatusDefectClassificationDetail.DefectClassificationItem",
                                "RequestPackageDetailStatus"
                            })

                        If oldData IsNot Nothing Then
                            If Not ValidateChanges(isQuality, ForceSave, oldData, lst) Then
                                _validations.Add(oldData?.RequestPackageDetailStatus?.BatchCode)
                                Continue For
                            End If

                            Dim updateHeaderInfo As Boolean = (requestPackageDetailStatusIds.Count > 1)
                            UpdateClassificationEntity(oldData, DefectClassificationHeader, lst, audit, isQuality, updateHeaderInfo)

                            _requestPackageDetailStatusDefectClassificationRepository.SaveEntity(oldData)

                        Else
                            Dim newEntity = CreateClassificationEntity(requestPackageDetailStatusId, DefectClassificationHeader, lst, audit, isQuality)
                            _requestPackageDetailStatusDefectClassificationRepository.SaveEntity(newEntity)
                        End If
                    Next

                Else
                    SaveDefectClasificationByRefillingAndRepackaging(requestPackageDetailStatusIds, lst, audit, DefectClassificationHeader, isQuality)
                End If

                If _validations.Count = 0 OrElse ForceSave Then
                    _requestPackageDetailStatusDefectClassificationRepository.UnitWork.Commit()
                    scope.Complete()
                Else
                    _requestPackageDetailStatusDefectClassificationRepository.UnitWork.Dispose()
                    scope.Dispose()
                    Return New ActionResult With {
                    .StateResult = False,
                    .MessageResult = _validations?.Distinct().ToList()}
                End If

                Return New ActionResult With {.StateResult = True}

            End Using

        Catch ex As IndigoValidationException
            Return New ActionResult With {.StateResult = False, .Message = ex.Message}

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {
            .StateResult = False,
            .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Valida si existen cambios relevantes entre los datos actuales y los nuevos datos de clasificación.
    ''' Esta función se utiliza para evitar sobreescritura innecesaria cuando no hay modificaciones significativas.
    ''' </summary>
    ''' <param name="isQuality">Indica si la validación corresponde al proceso de calidad (True) o producción (False).</param>
    ''' <param name="forceSave">Indica si se debe forzar el guardado, incluso si no hay cambios detectados.</param>
    ''' <param name="oldData">Lista de detalles existentes ya almacenados en la base de datos.</param>
    ''' <param name="lst">Lista de detalles nuevos recibidos como entrada del usuario.</param>
    ''' <returns>True si se permite continuar con la actualización (hay cambios o guardado forzado); False en caso contrario.</returns>
    Private Function ValidateChanges(
    isQuality As Boolean,
    ForceSave As Boolean,
    oldData As RequestPackageDetailStatusDefectClassification,
    lst As List(Of DefectClassificationModel)) As Boolean
        If ForceSave Then Return True

        For Each s In oldData.RequestPackageDetailStatusDefectClassificationDetail
            Dim inputItem = lst.FirstOrDefault(Function(m) m.DefectClassificationItemId = s.DefectClassificationItemId)

            If inputItem IsNot Nothing Then
                If isQuality AndAlso s.Production.HasValue AndAlso s.Production.Value AndAlso inputItem.Quality.HasValue AndAlso s.Production.Value <> inputItem.Quality.Value Then
                    Return False
                End If
                If Not isQuality AndAlso s.DefectClassificationItem IsNot Nothing AndAlso s.DefectClassificationItem.Critical Then
                    Return False
                End If
            End If
        Next

        Return True
    End Function

    ''' <summary>
    ''' Actualiza una entidad existente de clasificación de defectos con nueva información de encabezado y detalles.
    ''' </summary>
    ''' <param name="oldData">Entidad existente a modificar, del tipo RequestPackageDetailStatusDefectClassification.</param>
    ''' <param name="DefectClassificationHeader">Modelo que contiene los datos generales del encabezado (observación, pesos).</param>
    ''' <param name="lst">Lista de nuevos ítems de clasificación de defectos que se deben aplicar.</param>
    ''' <param name="audit">Información de auditoría, contiene el usuario actual y datos de seguimiento.</param>
    ''' <param name="isQuality">Indica si la actualización aplica a calidad (True) o producción (False).</param>
    ''' <param name="updateHeaderInfo">Define si se deben actualizar también los campos de encabezado (observación, usuario y fecha).</param>
    Private Sub UpdateClassificationEntity(
    oldData As RequestPackageDetailStatusDefectClassification,
    DefectClassificationHeader As DefectClassificationHeaderModel,
    lst As List(Of DefectClassificationModel),
    audit As AuditMessage,
    isQuality As Boolean,
    updateHeaderInfo As Boolean)
        If updateHeaderInfo Then
            oldData.Observation = If(DefectClassificationHeader.Observation, String.Empty)
            oldData.ModificationUser = audit.CodeUser
            oldData.ModificationDate = Date.Now
        End If

        oldData.ValidateWeigthNPT = DefectClassificationHeader.ValidateWeigthNPT
        oldData.ActualWeight = DefectClassificationHeader.ActualWeight

        For Each item In lst
            Dim existing = oldData.RequestPackageDetailStatusDefectClassificationDetail _
            .FirstOrDefault(Function(m) m.DefectClassificationItemId = item.DefectClassificationItemId)

            If existing IsNot Nothing Then
                If isQuality Then
                    existing.Quality = item.Quality
                Else
                    existing.Production = item.Production
                End If
            Else
                oldData.RequestPackageDetailStatusDefectClassificationDetail.Add(New RequestPackageDetailStatusDefectClassificationDetail With {
                .DefectClassificationItemId = item.DefectClassificationItemId,
                .Production = If(isQuality, Nothing, item.Production),
                .Quality = If(isQuality, item.Quality, Nothing)
            })
            End If
        Next

        oldData.MarkAsModified()
    End Sub

    ''' <summary>
    ''' Crea una nueva entidad de clasificación de defectos asociada a un RequestPackageDetailStatus.
    ''' </summary>
    ''' <param name="requestPackageDetailStatusId">Identificador del RequestPackageDetailStatus al que se asociará la clasificación.</param>
    ''' <param name="DefectClassificationHeader">Encabezado de clasificación que incluye observaciones y valores de peso.</param>
    ''' <param name="lst">Lista de ítems de clasificación de defectos a registrar (producción o calidad).</param>
    ''' <param name="audit">Información del usuario que realiza la operación (auditoría).</param>
    ''' <param name="isQuality">Indica si se está clasificando por calidad (True) o producción (False).</param>
    ''' <returns>Entidad completamente construida de tipo RequestPackageDetailStatusDefectClassification, lista para ser persistida.</returns>
    Private Function CreateClassificationEntity(
    requestPackageDetailStatusId As Integer,
    DefectClassificationHeader As DefectClassificationHeaderModel,
    lst As List(Of DefectClassificationModel),
    audit As AuditMessage,
    isQuality As Boolean) As RequestPackageDetailStatusDefectClassification

        Dim newEntity As New RequestPackageDetailStatusDefectClassification With {
        .RequestPackageDetailStatusId = requestPackageDetailStatusId,
        .Observation = If(DefectClassificationHeader.Observation, String.Empty),
        .CreationUser = audit.CodeUser,
        .CreationDate = Date.Now,
        .ValidateWeigthNPT = DefectClassificationHeader.ValidateWeigthNPT,
        .ActualWeight = DefectClassificationHeader.ActualWeight
    }

        For Each item In lst
            newEntity.RequestPackageDetailStatusDefectClassificationDetail.Add(New RequestPackageDetailStatusDefectClassificationDetail With {
            .DefectClassificationItemId = item.DefectClassificationItemId,
            .Production = If(isQuality, Nothing, item.Production),
            .Quality = If(isQuality, item.Quality, Nothing)
        })
        Next

        Return newEntity
    End Function

    ''' <summary>
    ''' Asigna clasificación de defectos para productos de tipo Reempaque/Reenvase,
    ''' distribuyendo la cantidad de defectos entre los elementos correspondientes.
    ''' </summary>
    ''' <param name="_requestPackageDetailStatusIdsTmp">Lista de IDs de detalles a los que se asignará la clasificación</param>
    ''' <param name="_lstTmp">Lista de defectos clasificados por tipo y cantidad</param>
    ''' <param name="_audit">Información de auditoría</param>
    ''' <param name="_DefectClassificationHeaderModelTmp">Datos generales de encabezado</param>
    ''' <param name="_isQuality">Indica si la asignación es para calidad o producción</param>
    Public Sub SaveDefectClasificationByRefillingAndRepackaging(
    _requestPackageDetailStatusIdsTmp As List(Of Integer),
    _lstTmp As List(Of DefectClassificationModel),
    _audit As AuditMessage,
    _DefectClassificationHeaderModelTmp As DefectClassificationHeaderModel,
    _isQuality As Boolean)

        Try
            Dim ListDefectClassification As New List(Of RequestPackageDetailStatusDefectClassification)
            Dim usedIds As New HashSet(Of Integer)()

            ' 1. Distribuir los defectos con cantidad específica
            For Each defect In _lstTmp.Where(Function(q) q.Quantity > 0)

                ' Tomar tantos IDs como la cantidad del defecto y que no hayan sido asignados aún
                Dim availableIds = _requestPackageDetailStatusIdsTmp.
                Where(Function(id) Not usedIds.Contains(id)).
                Take(defect.Quantity).ToList()

                For Each id In availableIds
                    ListDefectClassification.Add(
                    GetDetailsRequestPackageDetailStatusDefectClassification(
                        id,
                        _DefectClassificationHeaderModelTmp,
                        _audit.CodeUser,
                        _lstTmp,
                        _isQuality,
                        defect
                    )
                )
                    usedIds.Add(id)
                Next
            Next

            ' 2. Rellenar los faltantes (sin cantidad específica de defecto)
            Dim remainingIds = _requestPackageDetailStatusIdsTmp.Except(usedIds).ToList()
            For Each id In remainingIds
                ListDefectClassification.Add(
                GetDetailsRequestPackageDetailStatusDefectClassification(
                    id,
                    _DefectClassificationHeaderModelTmp,
                    _audit.CodeUser,
                    _lstTmp,
                    _isQuality
                )
            )
            Next
        Catch ex As Exception
            Throw ' Delegar manejo de errores al contexto llamador
        End Try
    End Sub

    ''' <summary>
    ''' Crea o actualiza la entidad de clasificación de defectos para un item de reempaque/reenvase.
    ''' </summary>
    ''' <param name="requestPackageDetailStatusIdTmp">ID del RequestPackageDetailStatus</param>
    ''' <param name="defectClassificationHeader">Modelo de encabezado con observación e indicaciones</param>
    ''' <param name="codeUser">Usuario actual</param>
    ''' <param name="listDefects">Lista completa de defectos a aplicar</param>
    ''' <param name="isQuality">True para calidad, False para producción</param>
    ''' <param name="defectClassification">Defecto específico actual (opcional)</param>
    ''' <returns>Entidad de clasificación creada o actualizada</returns>
    Public Function GetDetailsRequestPackageDetailStatusDefectClassification(
        requestPackageDetailStatusIdTmp As Integer,
        defectClassificationHeader As DefectClassificationHeaderModel,
        codeUser As String,
        listDefects As List(Of DefectClassificationModel),
        isQuality As Boolean,
        Optional defectClassification As DefectClassificationModel = Nothing
    ) As RequestPackageDetailStatusDefectClassification

        Dim classification = _requestPackageDetailStatusDefectClassificationRepository.FirstOrDefault(
            Function(m) m.RequestPackageDetailStatusId = requestPackageDetailStatusIdTmp,
            True,
            includes:={"RequestPackageDetailStatusDefectClassificationDetail"}
        )

        If classification Is Nothing Then
            classification = CreateNewClassification(requestPackageDetailStatusIdTmp, defectClassificationHeader, codeUser)
        Else
            UpdateClassificationHeader(classification, defectClassificationHeader, codeUser)
            classification.MarkAsModified()
        End If

        ' Procesa los detalles (crear o actualizar)
        ProcessClassificationDetails(classification, listDefects, isQuality, defectClassification)

        _requestPackageDetailStatusDefectClassificationRepository.SaveEntity(classification)
        Return classification
    End Function

    ''' <summary>
    ''' Crea la entidad principal RequestPackageDetailStatusDefectClassification
    ''' </summary>
    Private Function CreateNewClassification(
        requestPackageDetailStatusIdTmp As Integer,
        defectClassificationHeader As DefectClassificationHeaderModel,
        codeUser As String
    ) As RequestPackageDetailStatusDefectClassification

        Return New RequestPackageDetailStatusDefectClassification With {
            .RequestPackageDetailStatusId = requestPackageDetailStatusIdTmp,
            .Observation = If(defectClassificationHeader.Observation, String.Empty),
            .CreationUser = codeUser,
            .CreationDate = Date.Now,
            .ValidateWeigthNPT = Nothing,
            .IndicationSize = defectClassificationHeader.IndicationSize
        }
    End Function

    ''' <summary>
    ''' Actualiza la entidad RequestPackageDetailStatusDefectClassification
    ''' </summary>
    Private Sub UpdateClassificationHeader(
        ByRef classification As RequestPackageDetailStatusDefectClassification,
        defectClassificationHeader As DefectClassificationHeaderModel,
        codeUser As String
    )
        classification.IndicationSize = defectClassificationHeader.IndicationSize
        classification.Observation = If(defectClassificationHeader.Observation, String.Empty)
        classification.ModificationUser = codeUser
        classification.ModificationDate = Date.Now
    End Sub

    ''' <summary>
    ''' Procesa los detalles RequestPackageDetailStatusDefectClassificationDetail
    ''' </summary>
    Private Sub ProcessClassificationDetails(
        ByRef classification As RequestPackageDetailStatusDefectClassification,
        listDefects As List(Of DefectClassificationModel),
        isQuality As Boolean,
        defectClassification As DefectClassificationModel
    )
        Dim existingDetailsMap = classification.RequestPackageDetailStatusDefectClassificationDetail _
                            .ToDictionary(Function(d) d.DefectClassificationItemId)

        For Each defect In listDefects
            Dim existingDetail As RequestPackageDetailStatusDefectClassificationDetail = Nothing
            If existingDetailsMap.TryGetValue(defect.DefectClassificationItemId, existingDetail) Then
                UpdateDetailItem(existingDetail, defect, isQuality, defectClassification)
            Else
                Dim newDetail = CreateNewDetailItem(defect, isQuality, defectClassification)
                classification.RequestPackageDetailStatusDefectClassificationDetail.Add(newDetail)
            End If
        Next
    End Sub

    ''' <summary>
    ''' crea un detalle nuevo RequestPackageDetailStatusDefectClassificationDetail
    ''' </summary>
    Private Function CreateNewDetailItem(
        defect As DefectClassificationModel,
        isQualityCheck As Boolean,
        defectClassification As DefectClassificationModel
    ) As RequestPackageDetailStatusDefectClassificationDetail

        Dim isSelectedItem = (defectClassification IsNot Nothing AndAlso defect.DefectClassificationItemId = defectClassification.DefectClassificationItemId)
        Dim productionValue As Boolean? = Nothing
        Dim qualityValue As Boolean? = Nothing

        If isQualityCheck Then
            qualityValue = If(isSelectedItem, defect.Quality, False)
        Else
            productionValue = If(isSelectedItem, defect.Production, False)
        End If

        Return New RequestPackageDetailStatusDefectClassificationDetail With {
        .DefectClassificationItemId = defect.DefectClassificationItemId,
        .Production = productionValue,
        .Quality = qualityValue
    }
    End Function

    ''' <summary>
    ''' Actualiza el detalle RequestPackageDetailStatusDefectClassificationDetail
    ''' </summary>
    Private Sub UpdateDetailItem(
        ByRef existingDetail As RequestPackageDetailStatusDefectClassificationDetail,
        defect As DefectClassificationModel,
        isQualityCheck As Boolean,
        selectedItem As DefectClassificationModel
    )
        Dim isSelectedItem = (selectedItem IsNot Nothing AndAlso defect.DefectClassificationItemId = selectedItem.DefectClassificationItemId)

        If isQualityCheck Then
            existingDetail.Quality = If(isSelectedItem, defect.Quality, False)
        Else
            existingDetail.Production = If(isSelectedItem, defect.Production, False)
        End If
    End Sub

    ''' <summary>
    ''' Funcion para Cmabiar el estado de la campaña
    ''' </summary>
    ''' <param name="RequestPackageDetailStatusIds"></param>
    ''' <param name="arguments"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Async Function UpdateRequestPackageDetailStatusQSAsync(requestPackageDetailStatusIds As List(Of Integer), arguments As String, audit As AuditMessage) As Task(Of ActionResult) Implements IQualityControlAdminService.UpdateRequestPackageDetailStatusQSAsync
        Try
            If requestPackageDetailStatusIds Is Nothing OrElse Not requestPackageDetailStatusIds.Any() Then
                Throw New ArgumentNullException(NameOf(requestPackageDetailStatusIds), "Debe proporcionar IDs de estado")
            End If

            If String.IsNullOrEmpty(arguments) Then
                Throw New ArgumentNullException(NameOf(arguments), "Debe proporcionar argumentos de estado")
            End If

            ' Deserializar el objeto dinámico de entrada
            Dim data As Object = Utils.DeserializeJsonToObject(arguments)
            Dim status As Byte = CByte(data.status)
            Dim qualityStatus As Byte = CByte(data.qualityStatus)

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {
            .Timeout = TransactionManager.MaximumTimeout,
            .IsolationLevel = IsolationLevel.ReadCommitted}, TransactionScopeAsyncFlowOption.Enabled)
                Dim uow = _requestPackageDetailStatusRepository.UnitWork

                ' Lógica para Liberar o Rechazar con validación de defectos críticos
                If qualityStatus = 4 Then
                    Dim listWithDefects = _requestPackageDetailStatusRepository.GetByFilter(
                    Function(x) requestPackageDetailStatusIds.Contains(x.Id),
                    True,
                    {"RequestPackageDetailStatusDefectClassification.RequestPackageDetailStatusDefectClassificationDetail.DefectClassificationItem"}).ToList()

                    For Each item In listWithDefects
                        Dim hasCriticalDefect = item.RequestPackageDetailStatusDefectClassification.
                        FirstOrDefault()?.
                        RequestPackageDetailStatusDefectClassificationDetail?.
                        Any(Function(d) (d.Quality.GetValueOrDefault() AndAlso d.DefectClassificationItem.Critical) OrElse
                                         (d.Production.GetValueOrDefault() AndAlso d.DefectClassificationItem.Critical)) = True

                        item.Status = If(hasCriticalDefect, 5, status) ' 5 = Rechazado por defecto crítico
                        item.QualityStatus = If(hasCriticalDefect, 2, 1)
                        item.MarkAsModified()
                        _requestPackageDetailStatusRepository.SaveEntity(item)
                    Next
                Else
                    ' Lógica general de actualización de estado sin validación de defectos
                    Dim listStatus = _requestPackageDetailStatusRepository.GetListPackageDetailStatus(requestPackageDetailStatusIds, Nothing)

                    If listStatus Is Nothing OrElse listStatus.Count = 0 Then
                        Throw New ArgumentException("Productos no encontrados")
                    End If

                    For Each item In listStatus
                        item.Status = status
                        item.QualityStatus = qualityStatus
                        item.MarkAsModified()
                        _requestPackageDetailStatusRepository.SaveEntity(item)
                    Next
                End If

                Await uow.CommitAsync()
                scope.Complete()
            End Using

            Return New ActionResult With {
            .StateResult = True,
            .Message = String.Join(", ", requestPackageDetailStatusIds)}

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {
            .StateResult = False,
            .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Actualiza el almacén de despacho para los registros de RequestPackageDetailStatus especificados.
    ''' </summary>
    ''' <param name="warehouseAndStatusIds">Tuple con WarehouseId e IDs de RequestPackageDetailStatus a actualizar.</param>
    Public Async Function RequestPackageDetailStatusUpdateWarehouseAsync(
        warehouseAndStatusIds As Tuple(Of Integer, List(Of Integer))
        ) As Task(Of ActionResult) Implements IQualityControlAdminService.RequestPackageDetailStatusUpdateWarehouseAsync

        Try
            If warehouseAndStatusIds Is Nothing Then
                Throw New IndigoValidationException("Data no puede ser nulo.")
            End If

            Using scope As New TransactionScope(TransactionScopeOption.Required,
                                            New TransactionOptions() With {
                                                .Timeout = TransactionManager.MaximumTimeout,
                                                .IsolationLevel = IsolationLevel.ReadCommitted
                                            },
                                            TransactionScopeAsyncFlowOption.Enabled)

                ' Cargar los registros que se deben actualizar
                Dim statusList = _requestPackageDetailStatusRepository.
                                GetByFilter(Function(x) warehouseAndStatusIds.Item2.Contains(x.Id), False).
                                ToList()

                ' Actualizar el campo DispensingWarehouseId y marcar como modificado
                statusList.ForEach(Sub(x)
                                       x.DispensingWarehouseId = warehouseAndStatusIds.Item1
                                       x.MarkAsModified()
                                   End Sub)

                ' Guardar los cambios en la base de datos (Bulk Update)
                Await _requestPackageDetailStatusRepository.SaveEntityMassiveAsync(statusList)

                scope.Complete()
            End Using

            Return New ActionResult With {.StateResult = True}

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {
            .StateResult = False,
            .Message = IndigoManagementExceptions.GetExceptionDetails(ex)
        }
        End Try

    End Function

    ''' <summary>
    ''' funcion para validar si un producto puede ser rechazado, reprocesado o liberado
    ''' </summary>
    ''' <param name="requestPackageStatusIds"></param>
    ''' <param name="TypeAction"></param>
    ''' <returns></returns>
    Public Function ValidationDefectClassification(requestPackageStatusIds As List(Of Integer), TypeAction As Integer) As ActionResult(Of List(Of Integer)) Implements IQualityControlAdminService.ValidationDefectClassification
        If Not requestPackageStatusIds?.Any() Then
            Return New ActionResult(Of List(Of Integer)) With {
            .StateResult = False,
            .Message = "No están llegando elementos para validar"}
        End If

        Try
            Dim result = New ActionResult(Of List(Of Integer))()
            Dim query = _requestPackageDetailStatusDefectClassificationRepository.
            GetByFilter(Function(x) requestPackageStatusIds.Contains(x.RequestPackageDetailStatusId), False,
                        {"RequestPackageDetailStatusDefectClassificationDetail.DefectClassificationItem"}).ToList()

            ' Validación de existencia de clasificaciones
            Dim missingIds = requestPackageStatusIds.Except(query.Select(Function(f) f.RequestPackageDetailStatusId)).ToList()
            If missingIds.Any() OrElse Not query.Any() Then
                Dim failedItems = _requestPackageDetailStatusRepository.
                GetByFilter(Function(g) requestPackageStatusIds.Contains(g.Id), False,
                            {"Package.InventoryProduct"}).ToList()

                result.StateResult = False
                result.Message = "Los siguientes productos no poseen clasificación de defectos:"
                result.MessageResult = failedItems.Select(Function(h) $"{h.Package?.Name} - {h.BatchCode}").ToList()
                Return result
            End If

            ' Validación de defectos críticos si aplica
            If TypeAction = 3 Then
                Dim criticalDefectIds As New Concurrent.ConcurrentBag(Of Integer)

                Parallel.ForEach(query, Sub(p)
                                            If p.RequestPackageDetailStatusDefectClassificationDetail.Any(Function(x) x.Quality = True AndAlso
                                                x.DefectClassificationItem?.Critical = True AndAlso
                                                x.DefectClassificationItem?.State = True) Then
                                                criticalDefectIds.Add(p.RequestPackageDetailStatusId)
                                            End If
                                        End Sub)

                If criticalDefectIds.Any() Then
                    Dim criticalItems = _requestPackageDetailStatusRepository.
                    GetByFilter(Function(g) criticalDefectIds.Contains(g.Id), False,
                                {"Package.InventoryProduct"}).ToList()

                    result.StateResult = False
                    result.Message = "Los siguientes productos tienen defectos críticos:"
                    result.MessageResult = criticalItems.
                    Select(Function(h) $"{h.Package?.InventoryProduct?.Name} - N° Lote: {h.BatchCode}").ToList()
                    Return result
                End If

            ElseIf Not {1, 2, 4}.Contains(TypeAction) Then
                Return New ActionResult(Of List(Of Integer)) With {
                .StateResult = False,
                .Message = "La acción no se puede validar"}
            End If

            ' Validación exitosa
            result.StateResult = True
            result.Message = "Proceso de validación exitoso"
            result.MessageResult = New List(Of String)()
            result.ObjectEmbbeded = requestPackageStatusIds
            Return result

        Catch ex As Exception
            Return New ActionResult(Of List(Of Integer)) With {
            .StateResult = False,
            .Message = ex.Message}
        End Try
    End Function

    Public Function RescheduleRequestMixingStation(requestPackageDetailStatusIds As List(Of Integer), audit As AuditMessage) As ActionResult Implements IQualityControlAdminService.RescheduleRequestMixingStation
        Try
            Dim status = _requestPackageDetailStatusRepository.GetByFilter(Function(m) requestPackageDetailStatusIds.Contains(m.Id), includes:={"RequestMixingStationDetail.RequestMixingStation", "RequestMixingStationDetail.RequestMixingStationDetailPatients"})
            Dim codes As New List(Of String)()

            If status.Any() Then

                For Each s In status
                    s.Status = 6
                    's.QualityStatus = 6
                    Dim request = CloneRequest(s, audit)
                    request.Code = GetSequenceCode()
                    codes.Add(request.Code)
                    _requestMixingStationRepository.SaveEntity(request)
                Next

                Using scope As New TransactionScope(TransactionScopeOption.Required,
                                                    New TransactionOptions() With {
                                                    .Timeout = TransactionManager.MaximumTimeout,
                                                    .IsolationLevel = IsolationLevel.ReadCommitted
                                                    })
                    _requestMixingStationRepository.UnitWork.Commit()
                    scope.Complete()
                End Using
            Else
                Throw New IndigoValidationException("Datos no encontrados")
            End If

            Return New ActionResult With {.StateResult = True, .MessageResult = codes}
        Catch ex As IndigoValidationException
            Return New ActionResult With {.StateResult = False, .Message = ex.Message}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function GetSequenceCode() As String
        Dim seq As MixingStationSequenceDetail = Me._secuenseDetailRepository.FirstOrDefault(Function(m) m.MixingStationSequence.IdForm = "2224", True, includes:={"Sequense", "MixingStationSequence"})

        If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.MixingStationSequence.Sequential Then
            Dim res = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)
            If res IsNot Nothing AndAlso Not res.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                seq.Next += 1
                Me._secuenseDetailRepository.SaveEntity(seq)
                Return res
            Else
                Throw New IndigoValidationException(String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME))
            End If
        Else
            Throw New IndigoValidationException(String.Format(ResourceManager.GetString("SequenceFormNotFound"), FORM_NAME))
        End If
    End Function

    Private Function CloneRequest(s As RequestPackageDetailStatus, audit As AuditMessage) As RequestMixingStation
        Dim request = s.RequestMixingStationDetail.RequestMixingStation

        Dim header = New RequestMixingStation With {
            .Code = "",
            .Status = 2,
            .RequestUser = audit.CodeUser,
            .RequestDate = Date.Now,
            .CMConfigurationId = request.CMConfigurationId,
            .RequestMixingStationDetail = New Domain.Entities.TrackableCollection(Of RequestMixingStationDetail)()
        }

        Dim detail As New RequestMixingStationDetail With {
            .ATCId = s.RequestMixingStationDetail.ATCId,
            .PackageId = s.RequestMixingStationDetail.PackageId,
            .PackagePersonalizedId = s.RequestMixingStationDetail.PackagePersonalizedId,
            .UnitDoseTypeId = s.RequestMixingStationDetail.UnitDoseTypeId,
            .Quantity = s.RequestMixingStationDetail.Quantity,
            .Status = 1,
            .EntityId = s.Id,
            .EntityName = NameOf(RequestPackageDetailStatus),
            .CareCenterCode = s.RequestMixingStationDetail.CareCenterCode,
            .Source = s.RequestMixingStationDetail.Source,
            .ProductionLineId = s.RequestMixingStationDetail.ProductionLineId,'.CampaignDetailId = s.RequestMixingStationDetail.CampaignDetailId,
            .LabelType = s.RequestMixingStationDetail.LabelType,
            .SendTo = 0,'s.RequestMixingStationDetail.SendTo,
            .ConfirmationUser = audit.CodeUser,
            .ConfirmationDate = Date.Now,
            .RequestMixingStationDetailPatients = New Domain.Entities.TrackableCollection(Of RequestMixingStationDetailPatients)()
        }

        For Each i In s.RequestMixingStationDetail.RequestMixingStationDetailPatients
            Dim patient As New RequestMixingStationDetailPatients With {
                .PatientCode = i.PatientCode,
                .FunctionalUnitCode = i.FunctionalUnitCode,
                .Bed = i.Bed,
                .AdministrationRouteId = i.AdministrationRouteId,
                .Quantity = i.Quantity,
                .EntityId = i.EntityId,
                .EntityName = i.EntityName,
                .Status = i.Status,
                .CampaignDetailId = Nothing
            }
            detail.RequestMixingStationDetailPatients.Add(patient)
        Next

        header.RequestMixingStationDetail.Add(detail)

        Return header
    End Function

#Region "Orden de Traslado"
    ''' <summary>
    ''' Proceso Orden de Traslado 
    ''' </summary>
    ''' <param name="transferOrderlist"></param>
    ''' <param name="ProductDetailsList"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Async Function SaveOrderTransferAsync(
        transferOrderlist As TransferOrder,
        productDetailsList As List(Of ViewListFinalControlProductModel),
        audit As AuditMessage) As Task(Of ActionResult) Implements IQualityControlAdminService.SaveOrderTransferAsync

        Try
            ' Validaciones iniciales
            If transferOrderlist Is Nothing Then Throw New ArgumentNullException(NameOf(transferOrderlist))
            If productDetailsList Is Nothing OrElse Not productDetailsList.Any() Then Throw New ArgumentNullException(NameOf(productDetailsList))
            If audit Is Nothing Then Throw New ArgumentNullException(NameOf(audit))

            Using scope As New TransactionScope(TransactionScopeOption.Required,
                                            New TransactionOptions With {
                                                .Timeout = TransactionManager.MaximumTimeout,
                                                .IsolationLevel = IsolationLevel.ReadCommitted},
                                            TransactionScopeAsyncFlowOption.Enabled)

                ' Obtener IDs necesarios
                Dim campaignDetailIds = productDetailsList.Select(Function(m) m.CampaignDetailId).Distinct().ToList()

                ' Obtener TODOS los RequestPackageDetailStatusIds de TODOS los elementos
                Dim requestStatusIds As New List(Of Integer)()
                For Each product In productDetailsList
                    If product.RequestPackageDetailStatusIds IsNot Nothing AndAlso product.RequestPackageDetailStatusIds.Any() Then
                        requestStatusIds.AddRange(product.RequestPackageDetailStatusIds)
                    Else
                        requestStatusIds.Add(product.Id)
                    End If
                Next

                requestStatusIds = requestStatusIds.Distinct().ToList()

                If Not requestStatusIds.Any() Then
                    Throw New ArgumentNullException("Productos no encontrados")
                End If

                ' Construir y guardar orden de transferencia
                Dim orderDetails = CreateOrderTransferDetail(productDetailsList)
                Dim order = CreateOrdertransfer(transferOrderlist, orderDetails, audit.CodeUser)

                Dim result = _transferOrderAdminService.SaveTrasnferOrder(order, audit)
                If Not result.StateResult Then
                    Return New ActionResult With {.StateResult = False, .Message = result.Message}
                End If

                ' Actualizar estados de detalle de paquetes (optimizado con bulk update)
                Dim statusEntitiesToUpdate = _requestPackageDetailStatusRepository _
                    .GetByFilter(Function(m) requestStatusIds.Contains(m.Id), False) _
                    .ToList()

                For Each statusEntity In statusEntitiesToUpdate
                    statusEntity.SendTo = 1
                    statusEntity.MarkAsModified()
                Next

                If statusEntitiesToUpdate.Any() Then
                    Await _requestPackageDetailStatusRepository.SaveEntityMassiveAsync(statusEntitiesToUpdate)
                End If

                ' Guardar reportes de campaña
                For Each campaignId In campaignDetailIds
                    _campaignReportsAdminService.SaveReports(Of TransferOrder)(
                    campaignDetailId:=campaignId,
                    entityId:=result.ObjectEmbbeded.Id,
                    ProcessName:="FinishedProduct")
                Next

                Await _requestPackageDetailStatusRepository.UnitWork.CommitAsync()
                scope.Complete()

                Return New ActionResult With {
                .StateResult = True,
                .Message = result.Message,
                .MessageResult = result.MessageResultAux}
            End Using

        Catch ex As OptimisticConcurrencyException
            Return New ActionResult With {
            .StateResult = False,
            .MessageResult = {ex.Message}.ToList(),
            .Message = ResourceManager.GetString("ErrorConcurrence")}

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {
            .StateResult = False,
            .MessageResult = {ex.Message}.ToList(),
            .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Funcion Crear el Detalle de la Orden de traslado
    ''' </summary>
    ''' <param name="ProductDetailsList"></param>
    ''' <returns></returns>
    Private Function CreateOrderTransferDetail(ProductDetailsList As List(Of ViewListFinalControlProductModel)) As List(Of TransferOrderDetail)
        Dim transferOrderDetail = New List(Of TransferOrderDetail)

        For Each productGroup In ProductDetailsList.GroupBy(Function(p) p.InventoryProductId)
            Dim firstItem = productGroup.First()
            Dim isRepackaging = productGroup.Any(Function(p) p.MSClassUnitDoseType = EUnitDoseTypeClass.Repackaging)

            Dim orderDetail As New TransferOrderDetail With {
                .ProductId = productGroup.Key,
                .Description = firstItem.ProductFullName,
                .Value = firstItem.CostProduct,
                .ConsumptionUnit = firstItem.ConsumptionUnit,
                .CostProduct = firstItem.CostProduct,
                .Quantity = productGroup.Sum(Function(p) p.DeliveredQuantity),
                .InventoryQuantity = 0
            }

            ' Agrupación por inventario físico
            For Each inventoryGroup In productGroup.GroupBy(Function(p) p.PhysicalInventoryId)
                Dim physicalId = inventoryGroup.Key
                Dim quantity As Integer

                ' Sumar DeliveredQuantity en lugar de contar items
                If isRepackaging Then
                    ' Para reempaque: sumar InventoryQuantity de todos los items del grupo
                    quantity = inventoryGroup.Sum(Function(p) p.InventoryQuantity)
                Else
                    ' Para otros tipos: sumar DeliveredQuantity de todos los items del grupo
                    quantity = inventoryGroup.Sum(Function(p) p.DeliveredQuantity)
                End If

                Dim batchSerial = createTransferBatchSerialDetail(physicalId, quantity)
                orderDetail.TransferOrderDetailBatchSerial.Add(batchSerial)

                orderDetail.InventoryQuantity += quantity
            Next

            transferOrderDetail.Add(orderDetail)
        Next

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
    ''' Funcion crear Orden de traslado
    ''' </summary>
    ''' <param name="transferOrderlist"></param>
    ''' <param name="OrderTransferDetail"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Private Function CreateOrdertransfer(transferOrderlist As TransferOrder, OrderTransferDetail As List(Of TransferOrderDetail), userCode As String) As TransferOrder
        Dim order As New TransferOrder
        With order
            .Code = ""
            .OperatingUnitId = transferOrderlist.OperatingUnitId
            .DocumentDate = Date.Now
            .OrderType = transferOrderlist.OrderType
            .DispatchTo = transferOrderlist.DispatchTo
            .SourceWarehouseId = transferOrderlist.SourceWarehouseId
            .TargetWarehouseId = transferOrderlist.TargetWarehouseId
            .Description = transferOrderlist.Description
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
