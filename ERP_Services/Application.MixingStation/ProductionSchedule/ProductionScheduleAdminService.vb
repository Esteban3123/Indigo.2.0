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
Imports System.Text.RegularExpressions
Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources
Imports Microsoft.Extensions.Caching.Memory

Public Class ProductionScheduleAdminService
    Implements IProductionScheduleAdminService, Inject

    Private _ProductionScheduleRepository As IProductionScheduleRepository
    Private _campaignDetailRepository As ICampaignDetailRepository
    Private _requestMixingStationDetailRepository As IRequestMixingStationDetailRepository
    Private _requestCampaignDetailUsersRepository As ICampaignDetailUsersRepository
    Private _campaignDetailItemsRepository As ICampaignItemsRepository
    Private _batchSerialSettingRepository As IBatchSerialSettingRepository
    Private _batchSerialSequenceRepository As IBatchSerialSequenceRepository
    ReadOnly _operatingUnitRepository As IOperatingUnitRepository
    Private ReadOnly _cmConsecutiveRepository As IMixingStationConsecutiveRepository
    Private ReadOnly _requestPackageDetailStatusRepository As IRequestPackageDetailStatusRepository
    Private ReadOnly _readjustmentLogRepository As IReadjustmentLogRepository
    Private ReadOnly _packageRepository As IPackageRepository
    Private ReadOnly _cache As IMemoryCache


    Public Sub New(ProductionScheduleRepository As IProductionScheduleRepository,
                   requestPackageDetailStatusRepository As IRequestPackageDetailStatusRepository,
                   CampaignDetailRepository As ICampaignDetailRepository,
                   RequestMixingStationDetailRepository As IRequestMixingStationDetailRepository,
                   RequestCampaignDetailUsersRepository As ICampaignDetailUsersRepository,
                   operatingUnitRepository As IOperatingUnitRepository,
                   cmConsecutiveRepository As IMixingStationConsecutiveRepository,
                   campaignDetailItemsRepository As ICampaignItemsRepository, BatchSerialSettingRepository As IBatchSerialSettingRepository,
                   BatchSerialSequenceRepository As IBatchSerialSequenceRepository,
                   readjustmentLogRepository As IReadjustmentLogRepository,
                   packageRepository As IPackageRepository,
                   cache As IMemoryCache)
        If ProductionScheduleRepository Is Nothing Then
            Throw New ArgumentNullException("ProductionScheduleRepository vacío")
        End If
        If CampaignDetailRepository Is Nothing Then
            Throw New ArgumentNullException("CampaignDetailRepository vacío")
        End If
        If RequestMixingStationDetailRepository Is Nothing Then
            Throw New ArgumentNullException("RequestMixingStationDetailRepository vacío")
        End If
        _ProductionScheduleRepository = ProductionScheduleRepository
        _campaignDetailRepository = CampaignDetailRepository
        _requestMixingStationDetailRepository = RequestMixingStationDetailRepository
        _requestCampaignDetailUsersRepository = RequestCampaignDetailUsersRepository
        _campaignDetailItemsRepository = campaignDetailItemsRepository
        _operatingUnitRepository = operatingUnitRepository
        _cmConsecutiveRepository = cmConsecutiveRepository
        _requestPackageDetailStatusRepository = requestPackageDetailStatusRepository
        _batchSerialSettingRepository = BatchSerialSettingRepository
        _batchSerialSequenceRepository = BatchSerialSequenceRepository
        _readjustmentLogRepository = readjustmentLogRepository
        _packageRepository = packageRepository
        _cache = cache
    End Sub

    Public Function GetCampaignDetailIdsByBatchCode(cmConfigurationId As Integer, productionLineId As Integer, batchCode As String) As List(Of Integer) Implements IProductionScheduleAdminService.GetCampaignDetailIdsByBatchCode
        Try
            Dim campaignDetailIds = _readjustmentLogRepository.GetCampaignDetailIdsByBatchCode(cmConfigurationId, productionLineId, batchCode)
            Return campaignDetailIds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Validaciones de las campañas al momento de procesar
    ''' </summary>
    ''' <param name="campaignDetailIds"></param>
    ''' <returns></returns>
    Public Function ValidateProductionSchedule(campaignDetailIds As List(Of Integer)) As ActionResult Implements IProductionScheduleAdminService.ValidateProductionSchedule
        Try
            Dim sb As New StringBuilder()
            For Each campaignDetailId In campaignDetailIds
                Dim campaignDetail = _campaignDetailRepository _
                    .FirstOrDefault(Function(m) m.Id = campaignDetailId, False)

                If campaignDetail.Status < 3 Then
                    sb.AppendLine($"La campaña ({campaignDetail.CampaignNumber}) no ha superado el estado validacion de lotes")
                    Continue For
                End If

                Dim detailItems = _campaignDetailItemsRepository.GetProductsItemsValidation(campaignDetailId)

                If Not detailItems.Any() Then
                    sb.AppendLine($"No se encontraron items para la campaña ({campaignDetail.CampaignNumber})")
                    Continue For
                End If

                If detailItems.Any(Function(m) (m.RequestQuantity - m.DeliveredQuantity) > 0) Then
                    Dim str = String.Join(vbCrLf, detailItems.Where(Function(m) m.RequestQuantity - m.DeliveredQuantity > 0) _
                                          .Select(Function(m) If(m.ProductId.HasValue, $"{vbTab}* {m.InventoryProduct.Code} - {m.InventoryProduct.Name}",
                                          If(m.SupplyId.HasValue, $"{vbTab}* {m.InventorySupplie.Code} - {m.InventorySupplie.SupplieName}", $"{vbTab}* {m.ATC.Code} - {m.ATC.Name}"))).ToArray())
                    sb.AppendLine($"La campaña {campaignDetail.CampaignNumber} tiene los siguientes items pendientes por entregar: {vbCrLf}{str}")
                    Continue For
                End If
            Next

            If sb.Length > 0 Then Throw New IndigoValidationException(sb.ToString())

            Return New ActionResult With {.StateResult = True}
        Catch ex As IndigoValidationException
            Return New ActionResult With {.StateResult = False, .Message = ex.Message}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    '''  Guarda la orden de produccion
    ''' </summary>
    ''' <param name="objParams"></param>
    ''' <param name="authorizeUserslist"></param>
    ''' <param name="campaignDetail"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Async Function SaveProductionScheduleAsync(objParams As String, authorizeUserslist As List(Of CampaignDetailUsers), campaignDetail As CampaignDetail, audit As AuditMessage) As Task(Of ActionResult(Of SP_SaveProductionSchedule_Result)) Implements IProductionScheduleAdminService.SaveProductionScheduleAsync

        ' Validación de parámetros temprana
        If String.IsNullOrWhiteSpace(objParams) Then
            Return New ActionResult(Of SP_SaveProductionSchedule_Result) With {.StateResult = False, .Message = "Los parámetros de la orden de producción son requeridos"}
        End If
        If campaignDetail Is Nothing Then
            Return New ActionResult(Of SP_SaveProductionSchedule_Result) With {.StateResult = False, .Message = "El detalle de campaña es requerido"}
        End If
        If audit Is Nothing Then
            Return New ActionResult(Of SP_SaveProductionSchedule_Result) With {.StateResult = False, .Message = "La información de auditoría es requerida"}
        End If

        ' Deserialización fuera del TransactionScope
        Dim args As Object
        Try
            args = Utils.DeserializeJsonToObject(objParams)
        Catch ex As Exception
            Return New ActionResult(Of SP_SaveProductionSchedule_Result) With {
                .StateResult = False,
                .Message = $"Error al deserializar los parámetros: {ex.Message}"
            }
        End Try

        Dim scope As TransactionScope = Nothing
        Try
            scope = New TransactionScope(
                TransactionScopeOption.Required,
                New TransactionOptions With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted},
                TransactionScopeAsyncFlowOption.Enabled)

            Dim response As ActionResult(Of SP_SaveProductionSchedule_Result)

            Dim resSaveUsers As ActionResult(Of CampaignDetailUsers) = Await SaveAuthorizeUsersAsync(authorizeUserslist, campaignDetail, audit)
            If Not resSaveUsers.StateResult Then
                scope?.Dispose()
                Return New ActionResult(Of SP_SaveProductionSchedule_Result) With {.StateResult = False, .Message = resSaveUsers.Message}
            End If

            ' Ejecutar SP_SaveProductionSchedule si corresponde
            If campaignDetail.CampaignStatus = 2 Then
                Dim xml = ConvertEntityToXml(args)
                Dim result = _ProductionScheduleRepository.SP_SaveProductionSchedule(xml, audit.CodeUser)
                If result.CodeMessage <> 0 Then
                    scope?.Dispose()
                    Return New ActionResult(Of SP_SaveProductionSchedule_Result) With {.StateResult = False, .Message = result.Message}
                End If

                response = New ActionResult(Of SP_SaveProductionSchedule_Result) With {
                    .StateResult = True,
                    .ObjectEmbbeded = result,
                    .Message = result.Message
                }
            Else
                response = New ActionResult(Of SP_SaveProductionSchedule_Result) With {
                    .StateResult = True,
                    .ObjectEmbbeded = Nothing,
                    .Message = "Registro Actualizado correctamente",
                    .MessageAux = "Importante: Los cambios realizados generan cambios en las etiquetas. Asegúrese de realizar la reimpresión de etiquetas antes de continuar con la campaña"
                }
            End If

            Await _campaignDetailRepository.UnitWork.CommitAsync()
            scope.Complete()

            Return response
        Catch ex As IndigoValidationException
            scope?.Dispose()
            Return New ActionResult(Of SP_SaveProductionSchedule_Result) With {
                .StateResult = False,
                .Message = ex.Message
            }

        Catch ex As OptimisticConcurrencyException
            scope?.Dispose()
            Return New ActionResult(Of SP_SaveProductionSchedule_Result) With {
                .StateResult = False,
                .Message = "Los datos han sido modificados por otro usuario. Por favor, recargue la información e intente nuevamente."
            }

        Catch ex As Exception
            scope?.Dispose()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SP_SaveProductionSchedule_Result) With {
                .StateResult = False,
                .Message = $"Error al guardar la orden de producción: {Utils.GetInnerExceptionMessageToString(ex)}"
            }

        Finally
            scope?.Dispose()
        End Try
    End Function

    Private Function ConvertEntityToXml(args As Object) As Object
        Dim builder As New StringBuilder

        builder.Append("<Data>")
        builder.Append("<CMConfigurationId>" & args.CMConfigurationId & "</CMConfigurationId>")
        builder.Append("<ProductionLineId>" & args.ProductionLineId & "</ProductionLineId>")
        builder.Append("<OperatingUnitId>" & args.OperatingUnitId & "</OperatingUnitId>")

        For Each item In CType(args.Details, List(Of Object)).ToList()
            builder.Append("<Details>")
            builder.Append("<CampaignDetailId>" & item.CampaignDetailId & "</CampaignDetailId>")
            builder.Append("</Details>")
        Next

        builder.Append("</Data>")

        Return builder.ToString()
    End Function


    ''' <summary>
    ''' Variable tipo Nomeclatura Lote
    ''' </summary>
    Public msClassType As String

    ''' <summary>
    ''' Variable tipo Nomeclatura Ciudad
    ''' </summary>
    Public uoName As String

    ''' <summary>
    ''' Variable tipo Nomeclatura Fecha
    ''' </summary>
    Public BatchDate As String

    ''' <summary>
    ''' Variable tipo Nomeclatura Clase
    ''' </summary>
    Public campaignNomeclatura As String


    Public Function GetProductionSchedule(code As String, audit As AuditMessage) As ActionResult(Of ProductionSchedule) Implements IProductionScheduleAdminService.GetProductionSchedule
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim ProductionSchedule As ProductionSchedule = Me._ProductionScheduleRepository.GetProductionSchedule(code.Trim())
            If ProductionSchedule IsNot Nothing AndAlso ProductionSchedule.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of ProductionSchedule)(ProductionSchedule, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If

            Return New ActionResult(Of ProductionSchedule) With {.StateResult = True, .ObjectEmbbeded = ProductionSchedule}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ProductionSchedule) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function GetProductionScheduleById(id As Integer) As ActionResult(Of ProductionSchedule) Implements IProductionScheduleAdminService.GetProductionScheduleById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Dim ProductionSchedule As ProductionSchedule = Me._ProductionScheduleRepository.GetProductionScheduleById(id)
            Return New ActionResult(Of ProductionSchedule) With {.StateResult = True, .ObjectEmbbeded = ProductionSchedule}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ProductionSchedule) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Funcion para Cmabiar el estado de la campaña
    ''' </summary>
    ''' <param name="campaingDetailIds"></param>
    ''' <param name="action"></param>
    ''' <returns></returns>
    Public Async Function CampaingDetailStatusChangeAsync(campaingDetailIds As List(Of Integer), action As Byte, audit As AuditMessage) As Task(Of ActionResult) Implements IProductionScheduleAdminService.CampaingDetailStatusChangeAsync
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted}, TransactionScopeAsyncFlowOption.Enabled)
            Try
                If campaingDetailIds.Count < 0 Then
                    Throw New ArgumentNullException("CampaingDetailId")
                End If

                Dim rejectedCampaings As New List(Of Integer)
                Dim listRequestMixingStationDetail As New List(Of RequestMixingStationDetail)
                Dim listCampaingDetail = _campaignDetailRepository.GetCampaignsDetailByIds(campaingDetailIds)
                Dim campaignsToProcessIds = (From i In listCampaingDetail Where Not {action, 4, 5}.Contains(i.CampaignStatus) Select i.Id).Distinct().ToList()

                If Not campaignsToProcessIds.Any() Then
                    Return New ActionResult With {.StateResult = False, .Message = "Las Campañas selecionadas No estan en un estado valido para ejecutar la accion 'Cerrar Campaña'."}
                End If

                Select Case action
                    Case 1
                        rejectedCampaings = (From x In listCampaingDetail Where Not campaignsToProcessIds.Contains(x.Id) Select x.CampaignNumber).ToList()
                        listCampaingDetail = (From x In listCampaingDetail Where campaignsToProcessIds.Contains(x.Id) Select x).ToList()
                        listCampaingDetail.ForEach(Sub(CD As CampaignDetail)
                                                       If CD.CampaignStatus = 2 And CD.Status <> 1 Then
                                                           rejectedCampaings.Add(CD.CampaignNumber)
                                                           campaignsToProcessIds.Remove(CD.Id)
                                                       End If
                                                   End Sub
                                                   )
                        listCampaingDetail = (From x In listCampaingDetail Where campaignsToProcessIds.Contains(x.Id) Select x).ToList()
                    Case 2

                        listRequestMixingStationDetail = _requestMixingStationDetailRepository.GetRequestMixingStationDetailByCampaignDetailId(campaignsToProcessIds)
                        campaignsToProcessIds = (From x In listRequestMixingStationDetail Where x.CampaignDetailId IsNot Nothing Select x.CampaignDetailId.Value).Distinct.ToList()
                        rejectedCampaings = (From x In listCampaingDetail Where Not campaignsToProcessIds.Contains(x.Id) Select x.CampaignNumber).ToList()
                        listCampaingDetail = (From x In listCampaingDetail Where campaignsToProcessIds.Contains(x.Id) Select x).ToList()

                    Case 3
                        rejectedCampaings = (From x In listCampaingDetail Where Not campaignsToProcessIds.Contains(x.Id) Select x.CampaignNumber).ToList()
                        listCampaingDetail = (From x In listCampaingDetail Where campaignsToProcessIds.Contains(x.Id) Select x).ToList()

                    Case 4
                        listRequestMixingStationDetail = _requestMixingStationDetailRepository.GetRequestMixingStationDetailByCampaignDetailId(campaignsToProcessIds)

                        If listRequestMixingStationDetail.Any() Then
                            Dim campaignsWithRequest = listRequestMixingStationDetail.Select(Function(m) m.CampaignDetailId).Distinct().ToList()
                            Return New ActionResult With {.StateResult = False, .MessageResult = {$"Las campañas ({String.Join(", ", listCampaingDetail.Where(Function(m) campaignsWithRequest.Contains(m.Id)).Select(Function(m) m.CampaignNumber).ToArray())}) contienen solicitudes pendientes y no se pueden anular"}.ToList()}
                        End If

                        rejectedCampaings = (From x In listCampaingDetail Where Not campaignsToProcessIds.Contains(x.Id) Select x.CampaignNumber).ToList()
                        listCampaingDetail = (From x In listCampaingDetail Where campaignsToProcessIds.Contains(x.Id) Select x).ToList()
                End Select

                If Not listCampaingDetail.Any() Then
                    Return New ActionResult With {.StateResult = False, .Message = String.Join(", ", rejectedCampaings)}
                End If

                listCampaingDetail.ForEach(Sub(CD As CampaignDetail)
                                               With CD
                                                   .CampaignStatus = action
                                                   .ModificationDate = Date.Now
                                                   .ModificationUser = audit.CodeUser
                                                   .MarkAsModified()
                                               End With
                                               Me._campaignDetailRepository.SaveEntity(CD)
                                           End Sub)

                'Genero el lote a cada paquete de la campaña a cerrar(2).
                If action = 2 Then
                    Dim ResultGenBatchCode = Await GenerateBatchCodeAsync(listRequestMixingStationDetail, listCampaingDetail)
                    If Not ResultGenBatchCode.StateResult Then
                        scope.Dispose()
                        Return New ActionResult With {.StateResult = False, .Message = ResultGenBatchCode.Message}
                    End If
                End If

                Await _campaignDetailRepository.UnitWork.CommitAsync()
                scope.Complete()

                Return New ActionResult With {.StateResult = True, .Message = IIf(rejectedCampaings.Count > 0, String.Join(", ", rejectedCampaings), String.Empty)}

            Catch ex As Exception
                scope.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Funcion para generar el Lote a cada paquete de la campaña
    ''' </summary>
    ''' <param name="listRequestMixingStationDetail"></param>
    ''' <returns></returns>
    Private Async Function GenerateBatchCodeAsync(
        listRequestMixingStationDetail As List(Of RequestMixingStationDetail),
        listCampaignDetail As List(Of CampaignDetail)
        ) As Task(Of ActionResult)

        Using scope As New TransactionScope(TransactionScopeOption.Required,
                                        New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted},
                                        TransactionScopeAsyncFlowOption.Enabled)

            Try
                ' Validaciones iniciales
                If listRequestMixingStationDetail Is Nothing OrElse Not listRequestMixingStationDetail.Any(Function(x) x.RequestPackageDetailStatusId Is Nothing) Then
                    Return New ActionResult With {.StateResult = False, .Message = "El detalle de la solicitud llegó vacío"}
                End If

                listRequestMixingStationDetail = listRequestMixingStationDetail.
                                                Where(Function(x) x.RequestPackageDetailStatusId Is Nothing).
                                                ToList()

                Dim batchSerialSetting = _batchSerialSettingRepository.
                                        FirstOrDefault(Function(e) e.Id > 0, False, {"Sequense"})

                If batchSerialSetting Is Nothing Then
                    Return New ActionResult With {.StateResult = False, .Message = "No existen parámetros para generar el lote"}
                End If

                ' Consulta de datos
                Dim requestIds = String.Join(",", listRequestMixingStationDetail.Select(Function(x) x.Id).Distinct())
                Dim query = $"
                SELECT rpds.*
                FROM MixingStation.RequestPackageDetailStatus rpds
                JOIN MixingStation.RequestMixingStationDetail rmsd ON rpds.RequestMixingStationDetailId = rmsd.Id
                LEFT JOIN Inventory.ATC a ON a.Id = rmsd.ATCId
                LEFT JOIN MixingStation.Package p ON rmsd.PackageId = p.Id
                OUTER APPLY (SELECT TOP 1 pd.Quantity FROM MixingStation.PackageDetail pd WHERE pd.MainMedicine = 1 AND p.Id = pd.PackageId) pd 
                LEFT JOIN MixingStation.PackagePersonalized pp ON rpds.PackagePersonalizedId = pp.Id
                OUTER APPLY (SELECT TOP 1 ppd.Quantity FROM MixingStation.PackagePersonalizedDetail ppd WHERE ppd.MainMedicine = 1 AND pp.Id = ppd.PackagePersonalizedId) ppd 
                LEFT JOIN MixingStation.ConfirmationUnitDose cud ON cud.RequestMixingStationDetailId = rmsd.Id
                WHERE rpds.RequestMixingStationDetailId IN ({requestIds})
                ORDER BY a.Name, p.Name, IIF(rmsd.Source = 1, cud.Dosage, IIF(rpds.PackagePersonalizedId IS NOT NULL, ppd.Quantity, pd.Quantity))
            "

                Dim requestPackageStatuses = _requestPackageDetailStatusRepository.ExecuteQuery(Of RequestPackageDetailStatus)(query).ToList()
                If Not requestPackageStatuses.Any() Then
                    Return New ActionResult With {.StateResult = False, .Message = "La campaña no tiene paquetes asociados"}
                End If

                ' Secuencia para la fecha actual
                Dim batchSerialSequence = _batchSerialSequenceRepository.
                                          ExecuteQuery(Of BatchSerialSequence)("SELECT * FROM MixingStation.BatchSerialSequence WHERE BatchSerialSettingId = {0} AND SequenceDate = {1}",
                                                                               batchSerialSetting.Id, Date.Now.Date).
                                          FirstOrDefault()

                If batchSerialSequence Is Nothing Then
                    batchSerialSequence = _batchSerialSequenceRepository.
                                          ExecuteQuery(Of BatchSerialSequence)("INSERT INTO MixingStation.BatchSerialSequence OUTPUT inserted.* VALUES ({0}, 1, {1})",
                                                                               batchSerialSetting.Id, Date.Now.Date).
                                          FirstOrDefault()
                End If

                ' Datos adicionales
                Dim campaignDetailId = listRequestMixingStationDetail(0).CampaignDetailId
                Dim campaignDetail = _campaignDetailRepository.FirstOrDefault(Function(m) m.Id = campaignDetailId, includes:={"UnitDoseType"})
                Dim atcBatchCodes As New Dictionary(Of Integer, String)()
                Dim sharedBatchCodes As New Dictionary(Of Integer, String)()

                ' Generación de lotes
                For Each status In requestPackageStatuses
                    Dim detail = listRequestMixingStationDetail.First(Function(x) x.Id = status.RequestMixingStationDetailId)
                    Dim batchCode As String = String.Empty

                    'Solicitud Externa Maquila y solicitud de inventario deben tener el mismo lote
                    Dim isGrouped = {3, 4}.Contains(detail.Source) AndAlso detail.PackageId.HasValue AndAlso Not detail.PackagePersonalizedId.HasValue
                    If isGrouped AndAlso sharedBatchCodes.ContainsKey(detail.PackageId.Value) Then
                        batchCode = sharedBatchCodes(detail.PackageId.Value)
                    ElseIf {EUnitDoseTypeClass.Repackaging, EUnitDoseTypeClass.Refilling}.Contains(campaignDetail.UnitDoseType.MSClass) AndAlso atcBatchCodes.ContainsKey(detail.ATCId) Then
                        batchCode = atcBatchCodes(detail.ATCId)
                    Else
                        ' Generación completa
                        batchCode = GenerateFullBatchCode(batchSerialSetting, detail, listCampaignDetail, batchSerialSequence)
                        If isGrouped Then sharedBatchCodes(detail.PackageId.Value) = batchCode
                        If detail.ATCId IsNot Nothing AndAlso Not atcBatchCodes.ContainsKey(detail.ATCId) Then
                            atcBatchCodes(detail.ATCId) = batchCode
                        End If
                    End If

                    status.BatchCode = batchCode
                    status.StartTracking()
                    status.MarkAsModified()
                Next

                If requestPackageStatuses.Any(Function(y) String.IsNullOrWhiteSpace(y.BatchCode)) Then
                    Return New ActionResult With {.StateResult = False, .Message = "Existen detalles sin código de lote"}
                End If

                Await _requestPackageDetailStatusRepository.SaveEntityMassiveAsync(requestPackageStatuses)

                scope.Complete()
                Return New ActionResult With {.StateResult = True, .Message = "Se generaron correctamente los lotes"}

            Catch ex As Exception
                Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try

        End Using
    End Function

    ''' <summary>
    ''' General el BatchCode para los registros de requestpackagedetailstatus
    ''' </summary>
    ''' <param name="setting"></param>
    ''' <param name="detail"></param>
    ''' <param name="campaigns"></param>
    ''' <param name="sequence"></param>
    ''' <returns></returns>
    Private Function GenerateFullBatchCode(setting As BatchSerialSetting, detail As RequestMixingStationDetail, campaigns As List(Of CampaignDetail), sequence As BatchSerialSequence) As String
        Dim code As New StringBuilder()

        If setting.ActivateMixingStation Then
            Dim cmConfig = campaigns.First(Function(x) x.Id = detail.CampaignDetailId).Campaign.CMConfiguration
            code.Append(If(Not setting.CodeMSType.GetValueOrDefault(), cmConfig.Prefix, cmConfig.Code))
        End If

        If setting.ActivateUnitDoseType Then
            code.Append(If(Not setting.CodeUDTType.GetValueOrDefault(), detail.UnitDoseType.Prefix, detail.UnitDoseType.Code))
        End If

        code.Append(campaigns.First(Function(x) x.Id = detail.CampaignDetailId)?.CampaignNumber)

        If setting.DateFormatType.HasValue Then
            code.Append(FormatBatchDate(setting.DateFormatType.Value))
        End If

        If setting.SequenseId IsNot Nothing Then
            Dim pattern = setting.Sequense.Pattern
            Dim maxLength = Regex.Replace(setting.Sequense.Pattern, "[A-Za-z0-9]", String.Empty).Length
            If sequence.Next.ToString().Length > maxLength Then
                Throw New IndigoValidationException($"Longitud de secuencia superada. Patrón actual: {setting.Sequense.Name}")
            End If
            code.Append(sequence.Next.ToString(pattern).PadLeft(pattern.Length, "0"c))
            sequence.Next += 1
            sequence.MarkAsModified()
            _batchSerialSequenceRepository.SaveEntity(sequence)
        End If

        Return code.ToString()
    End Function

    ''' <summary>
    ''' Devuelve una fecha formateada para ser parte del código de lote, según configuración.
    ''' </summary>
    ''' <param name="formatType">1 = ddMMyy, 2 = yyMMdd, 3 = SerialDate desde 01/01/1900</param>
    Private Function FormatBatchDate(formatType As Integer?) As String
        Select Case formatType
            Case 1 : Return Date.Now.ToString("ddMMyy")
            Case 2 : Return Date.Now.ToString("yyMMdd")
            Case 3 : Return (DateDiff(DateInterval.Day, #1/1/1900#, Date.Now) + 2).ToString()
            Case Else : Return ""
        End Select
    End Function

    ''' <summary>
    ''' Funcion para Obtener los detalles de los paquetes por campaña  
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetItemsCampaigns(campaignDetailId As Integer, audit As AuditMessage) As ActionResult(Of SP_ListViewItemsCampaigns_Result) Implements IProductionScheduleAdminService.GetItemsCampaigns
        If campaignDetailId = 0 Then
            Throw New ArgumentNullException("campaignDetailId")
        End If
        Try
            Dim result = Me._ProductionScheduleRepository.SP_ListViewItemsCampaigns(campaignDetailId)
            Return New ActionResult(Of SP_ListViewItemsCampaigns_Result) With {.StateResult = True, .Data = result}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of SP_ListViewItemsCampaigns_Result) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function


    ''' <summary>
    ''' Procesa la orden de produccion
    ''' </summary>
    ''' <param name="AuthorizeUserslist"></param>
    ''' <param name="campaignDetail"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Async Function SaveAuthorizeUsersAsync(
        AuthorizeUserslist As List(Of CampaignDetailUsers),
        campaignDetail As CampaignDetail,
        audit As AuditMessage) As Task(Of ActionResult(Of CampaignDetailUsers)) Implements IProductionScheduleAdminService.SaveAuthorizeUsersAsync

        Try
            ' Validaciones iniciales críticas
            If campaignDetail Is Nothing Then
                Return New ActionResult(Of CampaignDetailUsers) With {
                    .StateResult = False,
                    .Message = "El detalle de campaña es requerido"
                }
            End If

            If Not campaignDetail.ProcessingDate.HasValue Then
                Return New ActionResult(Of CampaignDetailUsers) With {
                    .StateResult = False,
                    .Message = "La fecha de procesamiento es requerida para calcular las fechas de vencimiento"
                }
            End If

            If Not campaignDetail.PreparationTime.HasValue Then
                Return New ActionResult(Of CampaignDetailUsers) With {
                    .StateResult = False,
                    .Message = "El tiempo de preparación es requerido"
                }
            End If

            Dim campaignId = campaignDetail.Id
            _requestCampaignDetailUsersRepository.GetByFilter(Function(m) m.CampaignDetailId = campaignDetail.Id) _
                .ToList() _
                .ForEach(Sub(item) _requestCampaignDetailUsersRepository.DeleteEntity(item))

            'prepara la lista de nuevos usuarios.
            Dim newUsersToInsert = AuthorizeUserslist.Select(Function(au) New CampaignDetailUsers() With {
                    .CampaignDetailId = campaignId,
                    .UserId = au.UserId,
                    .UserCode = au.UserCode,
                    .UserRole = au.UserRole}).ToList
            ' Una única operación en la base de datos si hay datos para insertar.
            If newUsersToInsert.Any() Then
                Await _requestCampaignDetailUsersRepository.SaveEntityMassiveAsync(newUsersToInsert)
            End If

            ' Se ejecuta la función de cálculo de fechas de vencimiento
            Dim res As ActionResult = Await CalculateBatchPreparationTimeAndExpirationDateAsync(campaignDetail)
            If Not res.StateResult Then
                Return New ActionResult(Of CampaignDetailUsers) With {
                    .StateResult = False,
                    .Message = $"Error al calcular fechas de vencimiento: {res.Message}"
                }
            End If

            ' Verificación post-cálculo: Asegurar que todas las fechas se guardaron correctamente
            Dim verifyResult As ActionResult = Await VerifyExpirationDatesWereCalculated(campaignDetail.Id)
            If Not verifyResult.StateResult Then
                Return New ActionResult(Of CampaignDetailUsers) With {
                    .StateResult = False,
                    .Message = $"Verificación de fechas de vencimiento falló: {verifyResult.Message}"
                }
            End If

            Dim campaign = _campaignDetailRepository.FindById(campaignDetail.Id)
            campaign.CampaignStatus = 5
            campaign.ProcessingDate = campaignDetail.ProcessingDate
            campaign.PreparationTime = campaignDetail.PreparationTime
            _campaignDetailRepository.SaveEntity(campaign)

            Return New ActionResult(Of CampaignDetailUsers) With {.StateResult = True}
        Catch ex As OptimisticConcurrencyException
            Return New ActionResult(Of CampaignDetailUsers) With {
            .StateResult = False,
            .MessageResult = {ex.Message}.ToList,
            .Message = ResourceManager.GetString("ErrorConcurrence")
        }

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of CampaignDetailUsers) With {
            .StateResult = False,
            .MessageResult = {ex.Message}.ToList,
            .Message = IndigoManagementExceptions.GetExceptionDetails(ex)
        }
        End Try

    End Function

    ''' <summary>
    ''' Verifica que todas las fechas de vencimiento se hayan calculado correctamente después del proceso
    ''' Esta es una validación adicional de seguridad para garantizar integridad de datos
    ''' </summary>
    ''' <param name="campaignDetailId"></param>
    ''' <returns></returns>
    Private Async Function VerifyExpirationDatesWereCalculated(campaignDetailId As Integer) As Task(Of ActionResult)
        Try
            ' Consultar todos los items de la campaña y verificar que tengan fecha de vencimiento
            Dim itemsWithoutDate = Await _requestPackageDetailStatusRepository.Query(
                Function(rpds) rpds.RequestMixingStationDetail.CampaignDetailId = campaignDetailId AndAlso
                                Not rpds.BatchExpirationDate.HasValue,
                                includes:=New List(Of String) From {"RequestMixingStationDetail"}).ToListAsync()

            If itemsWithoutDate.Any() Then
                Dim batchCodes = String.Join(", ", itemsWithoutDate.Select(Function(i) i.BatchCode).ToArray())
                Return New ActionResult With {
                    .StateResult = False,
                    .Message = $"ERROR CRÍTICO: Los siguientes lotes NO tienen fecha de vencimiento después del cálculo: {batchCodes}. "
                }
            End If

            Return New ActionResult With {.StateResult = True}

        Catch ex As Exception
            Return New ActionResult With {
                .StateResult = False,
                .Message = $"Error al verificar las fechas de vencimiento: {ex.Message}"
            }
        End Try
    End Function

    ''' <summary>
    ''' Calcula el tiempo total de preparación y fecha de vencimiento
    ''' </summary>
    ''' <param name="campaingDetail"></param>
    ''' <returns></returns>
    Private Async Function CalculateBatchPreparationTimeAndExpirationDateAsync(campaingDetail As CampaignDetail) As Task(Of ActionResult)
        Try
            ' consulta LINQ-to-Entities.
            Dim listRequestPackageDetailStatus = Await _requestPackageDetailStatusRepository.Query(
                Function(rpds) rpds.RequestMixingStationDetail.CampaignDetailId = campaingDetail.Id,
                includes:=New List(Of String) From {"RequestMixingStationDetail"}).OrderBy(Function(rpds) rpds.BatchCode).ToListAsync()

            ' Se verifica si hay resultados antes de continuar.
            If Not listRequestPackageDetailStatus.Any() Then
                Return New ActionResult With {.StateResult = True} ' No hay nada que procesar, es un éxito.
            End If

            ' Se obtienen todos los paquetes necesarios en una sola consulta.
            Dim packageIds = listRequestPackageDetailStatus.Select(Function(s) s.PackageId).Distinct().ToList()
            Dim allPackages = _packageRepository.GetByFilter(Function(p) packageIds.Contains(p.Id))
            Dim packageDict = allPackages.ToDictionary(Function(p) p.Id) ' Diccionario para acceso rápido

            Dim dateReference = campaingDetail.ProcessingDate
            Dim currentTime As TimeSpan = dateReference.Value.TimeOfDay
            Dim BatchExpirationDateLater As Date = Nothing
            Dim IsMagistralOrNPT As Boolean = {EUnitDoseTypeClass.Magistral, EUnitDoseTypeClass.ParenteralNutrition}.Contains(campaingDetail.MSClass)
            Dim isRebottling As Boolean = {EUnitDoseTypeClass.Antibiotics, EUnitDoseTypeClass.Cytostatic, EUnitDoseTypeClass.OtherSterile}.Contains(campaingDetail.MSClass)
            Dim isRefilling As Boolean = {EUnitDoseTypeClass.Refilling}.Contains(campaingDetail.MSClass)
            Dim isRepackaging As Boolean = {EUnitDoseTypeClass.Repackaging}.Contains(campaingDetail.MSClass)

            Dim firstItem = listRequestPackageDetailStatus.First()
            Dim cacheRepackaging As New Dictionary(Of Integer, RequestPackageDetailStatus)
            Dim itemsToUpdate As New List(Of RequestPackageDetailStatus)() ' Lista para SaveEntityMassive optimizado

            For Each item As RequestPackageDetailStatus In listRequestPackageDetailStatus
                Try
                    ' La lógica de cálculo del tiempo de preparación se mantiene intacta.
                    If item Is firstItem Then
                        item.PreparationTime = currentTime
                    Else
                        currentTime = currentTime.Add(campaingDetail.PreparationTime)
                        If currentTime >= TimeSpan.FromHours(24) Then
                            Return New ActionResult With {.StateResult = False, .Message = $"El tiempo de preparación excede las 24 horas, para el lote: {item.BatchCode}"}
                        End If
                        item.PreparationTime = currentTime
                    End If

                    'Logica Estabilidad
                    ' Para tipos de dosis oncologicos (Citostáticos), cuando el preparationType es 4 (Ninguna)- Intratecales
                    Dim isCytostaticWithNoPreparation As Boolean = False
                    If campaingDetail.MSClass = EUnitDoseTypeClass.Cytostatic Then
                        Dim packageTmp As Package = Nothing
                        If item.PackageId.HasValue AndAlso packageDict.TryGetValue(item.PackageId.Value, packageTmp) Then
                            If packageTmp.PreparationType.HasValue AndAlso packageTmp.PreparationType.Value = 4 Then
                                isCytostaticWithNoPreparation = True
                            End If
                        End If
                    End If

                    If isRebottling AndAlso Not isCytostaticWithNoPreparation Then
                        Dim requestPackageDetail As ActionResult(Of RequestPackageDetailStatus) = GetHoursStabilityRebottling(item.Id, dateReference)
                        If requestPackageDetail.StateResult Then
                            item.BatchExpirationDate = requestPackageDetail.ObjectEmbbeded.BatchExpirationDate
                        Else
                            Return New ActionResult With {.StateResult = False, .Message = $"Error al calcular fecha de vencimiento para el lote {item.BatchCode}: {requestPackageDetail.Message}"}
                        End If
                    End If

                    'Logica reenvase
                    If isRefilling Then
                        Dim result As RequestPackageDetailStatus = Nothing
                        If cacheRepackaging.ContainsKey(item.RequestMixingStationDetail?.ATCId) Then
                            result = cacheRepackaging(item.RequestMixingStationDetail?.ATCId)
                        Else
                            Dim requestPackageDetail As ActionResult(Of RequestPackageDetailStatus) = GetHoursStabilityRefilling(item.Id, dateReference, item.RequestMixingStationDetail?.ATCId)
                            If requestPackageDetail.StateResult Then
                                result = requestPackageDetail.ObjectEmbbeded
                                cacheRepackaging(item.RequestMixingStationDetail?.ATCId) = result
                            Else
                                Return New ActionResult With {.StateResult = False, .Message = $"Error al calcular fecha de vencimiento para el lote {item.BatchCode}: {requestPackageDetail.Message}"}
                            End If
                        End If
                        item.BatchExpirationDate = result.BatchExpirationDate
                    End If

                    'Logica reempaque
                    If isRepackaging Then
                        Dim result As RequestPackageDetailStatus = Nothing
                        If cacheRepackaging.ContainsKey(item.RequestMixingStationDetail?.ATCId) Then
                            result = cacheRepackaging(item.RequestMixingStationDetail?.ATCId)
                        Else
                            Dim requestPackageDetail As ActionResult(Of RequestPackageDetailStatus) = GetHoursStabilityRepackaging(item?.RequestMixingStationDetailId, item.RequestMixingStationDetail?.ATCId)
                            If requestPackageDetail.StateResult Then
                                result = requestPackageDetail.ObjectEmbbeded
                                cacheRepackaging(item.RequestMixingStationDetail?.ATCId) = result
                            Else
                                Return New ActionResult With {.StateResult = False, .Message = $"Error al calcular fecha de vencimiento para el lote {item.BatchCode}: {requestPackageDetail.Message}"}
                            End If
                        End If
                        item.BatchExpirationDate = result.SupplierBatchExpirationDate
                    End If

                    'Logica Npt, magistral, Citostáticos intratecales
                    If IsMagistralOrNPT OrElse isCytostaticWithNoPreparation Then
                        Dim packageTmp As Package = Nothing
                        If Not item.PackageId.HasValue OrElse Not packageDict.TryGetValue(item.PackageId.Value, packageTmp) Then
                            Return New ActionResult With {.StateResult = False, .Message = $"No se encontró el paquete con ID {item.PackageId} para el lote {item.BatchCode}"}
                        End If

                        If packageTmp.TypeStability Is Nothing Then
                            Return New ActionResult With {.StateResult = False, .Message = $"La adecuación con lote {item.BatchCode} no tiene estabilidad en su paquete ({String.Concat(packageTmp.Code, "-", packageTmp.Name)})"}
                        End If

                        Dim Stability As DateTime? = Nothing
                        Select Case packageTmp.TypeStability.Value
                            Case 1 ' Solo horas
                                If packageTmp.StabilityHour.HasValue Then
                                    Stability = dateReference.Value.Add(packageTmp.StabilityHour)
                                End If
                            Case 2 ' Solo días
                                If packageTmp.StabilityDays.HasValue Then
                                    Stability = dateReference.Value.AddDays(packageTmp.StabilityDays)
                                End If
                            Case 3 ' Días + horas
                                If packageTmp.StabilityDays.HasValue AndAlso packageTmp.StabilityHour.HasValue Then
                                    Stability = dateReference.Value.AddDays(packageTmp.StabilityDays).Add(packageTmp.StabilityHour)
                                End If
                            Case Else
                                Return New ActionResult With {.StateResult = False, .Message = $"La adecuación con el lote {item.BatchCode} no tiene un tipo de estabilidad válido en su paquete ({packageTmp.Code}-{packageTmp.Name})"}
                        End Select

                        If Not Stability.HasValue Then
                            Return New ActionResult With {.StateResult = False, .Message = $"No se pudo calcular la estabilidad para el lote {item.BatchCode}"}
                        End If

                        If item Is firstItem Or isCytostaticWithNoPreparation Then
                            item.BatchExpirationDate = Stability
                            BatchExpirationDateLater = item.BatchExpirationDate
                        Else
                            item.BatchExpirationDate = BatchExpirationDateLater.Add(campaingDetail.PreparationTime)
                        End If
                    End If

                    ' Validación crítica: Verificar que se haya calculado la fecha de vencimiento
                    If Not item.BatchExpirationDate.HasValue Then
                        Return New ActionResult With {
                            .StateResult = False,
                            .Message = $"No se pudo calcular la fecha de vencimiento para el lote {item.BatchCode}. " &
                                      $"Tipo de preparación: {campaingDetail.MSClass}. " &
                                      $"Verifique que el paquete tenga configurada la estabilidad correctamente."
                        }
                    End If

                    ' Agregar item modificado a la lista para SaveEntityMassive
                    item.MarkAsModified()
                    itemsToUpdate.Add(item)

                Catch ex As Exception
                    Return New ActionResult With {.StateResult = False, .Message = $"Error procesando el lote {item.BatchCode}: {ex.Message}"}
                End Try
            Next

            ' Validación final: Verificar que todos los items tengan fecha de vencimiento (doble check de seguridad)
            Dim itemsWithoutDate = itemsToUpdate.Where(Function(i) Not i.BatchExpirationDate.HasValue).ToList()
            If itemsWithoutDate.Any() Then
                Dim batchCodes = String.Join(", ", itemsWithoutDate.Select(Function(i) i.BatchCode).ToArray())
                Return New ActionResult With {
                    .StateResult = False,
                    .Message = $"Validación final falló. Los siguientes lotes no tienen fecha de vencimiento asignada: {batchCodes}. "
                }
            End If

            ' OPTIMIZACIÓN CRÍTICA: Una sola operación masiva con manejo de errores mejorado
            If itemsToUpdate.Any() Then
                Try
                    Await _requestPackageDetailStatusRepository.SaveEntityMassiveAsync(itemsToUpdate)
                Catch ex As Exception
                    Return New ActionResult With {
                        .StateResult = False,
                        .Message = $"Error al guardar las fechas de vencimiento en la base de datos: {ex.Message}. " &
                                  $"Total de items: {itemsToUpdate.Count}"
                    }
                End Try
            End If

            Return New ActionResult With {.StateResult = True}
        Catch ex As Exception
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try

    End Function

    ''' <summary>
    ''' Logica para calcular la fecha de vencimiento por tabla Estabilidad
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="processingDate"></param>
    ''' <returns></returns>
    Private Function GetHoursStabilityRebottling(id As Integer, processingDate As DateTime) As ActionResult(Of RequestPackageDetailStatus)
        Dim query = $"
                    SELECT 
                    DATEADD(HOUR, ISNULL(stdd.HourStability, stdr.HourStability), @ProcessingDate) AS BatchExpirationDate, 
                    a.Code + ' - ' + a.Name AS NameMainMedicinePackage
                    FROM  MixingStation.RequestMixingStationDetail rmsd 
                    INNER  JOIN MixingStation.RequestPackageDetailStatus as rpds on rpds.RequestMixingStationDetailId = rmsd.Id
                    LEFT JOIN MixingStation.PackagePersonalizedDetail pp ON rmsd.PackagePersonalizedId = pp.PackagePersonalizedId AND pp.MainMedicine = 1
                    LEFT JOIN MixingStation.PackageDetail as p on p.PackageId = rmsd.PackageId AND p.MainMedicine = 1
                    LEFT JOIN MixingStation.PackagePersonalizedDetail as pp1 on pp1.PackagePersonalizedId = rmsd.PackagePersonalizedId AND pp1.Vehicle = 1
                    LEFT JOIN MixingStation.PackageDetail as p1 on p1.PackageId = rmsd.PackageId AND p1.Vehicle = 1
                    LEFT JOIN MixingStation.PackagePersonalizedDetail as pp2 on pp2.PackagePersonalizedId = rmsd.PackagePersonalizedId AND pp2.Thinner = 1
                    LEFT JOIN MixingStation.PackageDetail as p2 on p2.PackageId = rmsd.PackageId AND p2.Thinner = 1
                    LEFT JOIN MixingStation.StabilityTableDetail std ON std.ATCId = ISNULL(pp.AtcId,p.AtcId)
                    LEFT JOIN MixingStation.StabilityTableDetailDilution stdd ON std.Id = stdd.StabilityTableDetailId AND stdd.ATCId = ISNULL(pp1.AtcId, p1.AtcId)
                    LEFT JOIN MixingStation.StabilityTableDetailReconstitution stdr ON std.Id = stdr.StabilityTableDetailId AND stdr.ATCId = ISNULL(pp2.AtcId, p2.AtcId)
                    LEFT JOIN Inventory.ATC as a on a.Id = std.ATCId
                    WHERE rpds.Id  = @Id"
        Dim parameters As IEnumerable(Of (String, Object)) = {
            ("@ProcessingDate", processingDate),
            ("@Id", id)
        }
        Dim result As RequestPackageDetailStatus = _requestPackageDetailStatusRepository.ExecuteQueryDR(Of RequestPackageDetailStatus)(query, parameters).FirstOrDefault()
        If result.BatchExpirationDate Is Nothing Then
            Return New ActionResult(Of RequestPackageDetailStatus) With {.StateResult = False, .Message = $"el medicamento {result.NameMainMedicinePackage} no tiene un diluyente registrado en la tabla de estabilidad"}
        End If
        Return New ActionResult(Of RequestPackageDetailStatus) With {.StateResult = True, .ObjectEmbbeded = result}
    End Function

    ''' <summary>
    ''' Logica para calcular la fecha de vencimiento para reenvase
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="processingDate"></param>
    ''' <returns></returns>
    Private Function GetHoursStabilityRefilling(id As Integer, processingDate As DateTime, ATCid As Integer) As ActionResult(Of RequestPackageDetailStatus)
        Dim query = $"
            WITH CTE_ProductsCampaignDetail AS (
                SELECT 
                    cd.Id AS CampaignDetailId,
                    ip.Id AS ProductId,
                    ip.Code AS ProductCode,
                    ip.Name AS ProductName,
                    ip.HealthRegistration,
                    bs.BatchCode AS ProductBatchCode,
                    bs.ExpirationDate,
                    a.AbbreviationName AS ProductAbbreviation,
                    a.Id AS AtcId,
                    cdv.ItemType
                FROM MixingStation.CampaignDetailValidation cdv
                JOIN MixingStation.CampaignDetail cd ON cd.Id = cdv.CampaignDetailId
                JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON cdv.ProductId = ip.Id
                JOIN Inventory.ATC a ON a.Id = ip.ATCId
                JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON bs.Id = cdv.BatchSerialId
            )

            SELECT 
                CASE 
                    WHEN DATEDIFF(DAY, @ProcessingDate, cte.ExpirationDate) < 180
                        THEN DATEADD(
                        DAY, 
                        ROUND(DATEDIFF(DAY, @ProcessingDate, cte.ExpirationDate) * 0.25, 0), 
                        @ProcessingDate)
                    WHEN DATEDIFF(DAY, @ProcessingDate, cte.ExpirationDate) >= 180 
                        THEN CASE 
                                WHEN ROUND(DATEDIFF(DAY, @ProcessingDate, cte.ExpirationDate) * 0.25, 0) < 180
                                    THEN    DATEADD(
                                            DAY, 
                                            ROUND(DATEDIFF(DAY, @ProcessingDate, cte.ExpirationDate) * 0.25, 0), 
                                            @ProcessingDate)
                                ELSE    
                                    DATEADD(DAY, 180, @ProcessingDate)
                             END
                END AS BatchExpirationDate,
                cte.ProductCode + ' - ' + cte.ProductName AS NameMainMedicinePackage,
                cte.ExpirationDate AS SupplierBatchExpirationDate
            FROM MixingStation.RequestPackageDetailStatus rpds
            JOIN MixingStation.RequestMixingStationDetail rmsd ON rmsd.Id = rpds.RequestMixingStationDetailId
            JOIN MixingStation.CampaignDetail cd ON cd.Id = rmsd.CampaignDetailId
            JOIN CTE_ProductsCampaignDetail cte ON cte.CampaignDetailId = cd.Id AND cte.AtcId = rmsd.ATCId
            WHERE rpds.Id  = @Id  and rmsd.ATCId = @ATCId"
        Dim parameters As IEnumerable(Of (String, Object)) = {
            ("@ProcessingDate", processingDate),
            ("@Id", id),
            ("@ATCId", ATCid)
        }
        Dim result As RequestPackageDetailStatus = _requestPackageDetailStatusRepository.ExecuteQueryDR(Of RequestPackageDetailStatus)(query, parameters).FirstOrDefault()
        If result.BatchExpirationDate IsNot Nothing Then
            If result.SupplierBatchExpirationDate < Date.Now.Date Then
                Return New ActionResult(Of RequestPackageDetailStatus) With {.StateResult = False, .Message = $"El producto: {result.NameMainMedicinePackage} se encuentra vencido desde el {result.SupplierBatchExpirationDate}"}
            End If
        End If
        Return New ActionResult(Of RequestPackageDetailStatus) With {.StateResult = True, .ObjectEmbbeded = result}
    End Function

    ''' <summary>
    ''' Logica para calcular la fecha de vencimiento para remmpaque
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="ATCid"></param>
    ''' <returns></returns>
    Private Function GetHoursStabilityRepackaging(id As Integer, ATCid As Integer) As ActionResult(Of RequestPackageDetailStatus)
        Dim query = $"
            WITH CTE_ProductsCampaignDetail AS (
                SELECT 
                    cd.Id AS CampaignDetailId,
                    ip.Id AS ProductId,
                    ip.Code AS ProductCode,
                    ip.Name AS ProductName,
                    ip.HealthRegistration,
                    bs.BatchCode AS ProductBatchCode,
                    bs.ExpirationDate,
                    a.AbbreviationName AS ProductAbbreviation,
                    a.Id AS AtcId,
                    cdv.ItemType
                FROM MixingStation.CampaignDetailValidation cdv
                JOIN MixingStation.CampaignDetail cd ON cd.Id = cdv.CampaignDetailId
                JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON cdv.ProductId = ip.Id
                JOIN Inventory.ATC a ON a.Id = ip.ATCId
                JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON bs.Id = cdv.BatchSerialId
            )
            SELECT 
                cte.ExpirationDate AS SupplierBatchExpirationDate
             FROM  MixingStation.RequestMixingStationDetail rmsd
            JOIN MixingStation.CampaignDetail cd ON cd.Id = rmsd.CampaignDetailId
            JOIN CTE_ProductsCampaignDetail cte ON cte.CampaignDetailId = cd.Id AND cte.AtcId = rmsd.ATCId
            WHERE rmsd.Id  = @Id  and rmsd.ATCId = @ATCId"
        Dim parameters As IEnumerable(Of (String, Object)) = {
            ("@Id", id),
            ("@ATCId", ATCid)
        }
        Dim result As RequestPackageDetailStatus = _requestPackageDetailStatusRepository.ExecuteQueryDR(Of RequestPackageDetailStatus)(query, parameters).FirstOrDefault()
        If result.SupplierBatchExpirationDate IsNot Nothing Then
            If result.SupplierBatchExpirationDate < Date.Now.Date Then
                Return New ActionResult(Of RequestPackageDetailStatus) With {.StateResult = False, .Message = $"El producto: {result.NameMainMedicinePackage} se encuentra vencido desde el {result.SupplierBatchExpirationDate}"}
            End If
        End If
        Return New ActionResult(Of RequestPackageDetailStatus) With {.StateResult = True, .ObjectEmbbeded = result}
    End Function


#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If
            _ProductionScheduleRepository = Nothing
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
