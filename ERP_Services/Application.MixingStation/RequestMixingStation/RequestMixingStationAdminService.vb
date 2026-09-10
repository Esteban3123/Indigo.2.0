'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Text
Imports System.Transactions
Imports Application.Base
Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

Public Class RequestMixingStationAdminService
    Implements IRequestMixingStationAdminService, Inject

    Private _RequestMixingStationRepository As IRequestMixingStationRepository
    Private _requestMixingStationDetailRepository As IRequestMixingStationDetailRepository
    Private _pharmaDoseRepository As IPharmaDoseRepository
    Private _productSusceptibleMixingStationRepository As IProductSusceptibleMixingStationRepository
    Private _requestPackageDetailStatusRepository As IRequestPackageDetailStatusRepository
    Private _readjustmentsRepository As IReadjustmentsRepository
    Private ReadOnly _readjustmentLogAdminService As IReadjustmentLogAdminService

    Public Sub New(RequestMixingStationRepository As IRequestMixingStationRepository,
                   requestMixingStationDetailRepository As IRequestMixingStationDetailRepository,
                   pharmaDoseRepository As IPharmaDoseRepository,
                   readjustmentLogAdminService As IReadjustmentLogAdminService,
                   productSusceptibleMixingStationRepository As IProductSusceptibleMixingStationRepository, RequestPackageDetailStatusRepository As IRequestPackageDetailStatusRepository,
                   ReadjustmentsRepository As IReadjustmentsRepository)
        If RequestMixingStationRepository Is Nothing Then
            Throw New ArgumentNullException("RequestMixingStationRepository vacío")
        End If
        If requestMixingStationDetailRepository Is Nothing Then
            Throw New ArgumentNullException("requestMixingStationDetailRepository vacío")
        End If
        _pharmaDoseRepository = pharmaDoseRepository
        _RequestMixingStationRepository = RequestMixingStationRepository
        _requestMixingStationDetailRepository = requestMixingStationDetailRepository
        _productSusceptibleMixingStationRepository = productSusceptibleMixingStationRepository
        _requestPackageDetailStatusRepository = RequestPackageDetailStatusRepository
        _readjustmentLogAdminService = readjustmentLogAdminService
        _readjustmentsRepository = ReadjustmentsRepository
    End Sub

    Public Function SP_ProcessMixingStation(objParams As String, audit As AuditMessage) As ActionResult(Of SP_ProcessMixingStation_Result) Implements IRequestMixingStationAdminService.SP_ProcessMixingStation
        Dim args As Object = Utils.DeserializeJsonToObject(objParams)
        Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
            Try
                Dim xml = ConvertEntityToXml(args)

                Dim result = _RequestMixingStationRepository.SP_ProcessMixingStation(xml, audit.CodeUser)
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

    Public Function SaveRequestMixingStation(data As Tuple(Of Integer, Integer)) As ActionResult(Of RequestMixingStation) Implements IRequestMixingStationAdminService.SaveRequestMixingStation
        If data Is Nothing Then
            Throw New ArgumentNullException("data")
        End If
        Dim unitOfWork As IUnitWork = Me._requestMixingStationDetailRepository.UnitWork
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim requestMixingStationDetail = _requestMixingStationDetailRepository.GetRequestMixingStationDetailById(data.Item1)
                requestMixingStationDetail.ProductionLineId = data.Item2
                requestMixingStationDetail.MarkAsModified()

                Me._requestMixingStationDetailRepository.SaveEntity(requestMixingStationDetail)
                unitOfWork.Commit()

                'Se marca la entidad como sin cambios
                requestMixingStationDetail.MarkAsUnchanged()
                scope.Complete()
                Return New ActionResult(Of RequestMixingStation) With {.StateResult = True, .ObjectEmbbeded = Nothing}
            End Using

        Catch ex As Exception
            unitOfWork.RollbackChanges()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RequestMixingStation) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Private Function ConvertEntityToXml(args As Object) As Object
        Dim builder As New StringBuilder

        For Each item In CType(args.Details, List(Of Object)).ToList()
            builder.Append("<Data>")

            builder.Append("<RequestMixingStationDetailPatientsId>" & item.RequestMixingStationDetailPatientsId & "</RequestMixingStationDetailPatientsId>")
            builder.Append("<RequestMixingStationDetailId>" & item.RequestMixingStationDetailId & "</RequestMixingStationDetailId>")
            builder.Append("<RequestType>" & item.RequestType & "</RequestType>")
            If item.CodeSusceptibleMixingStation IsNot Nothing Then
                builder.Append("<CodeSusceptibleMixingStation>" & item.CodeSusceptibleMixingStation & "</CodeSusceptibleMixingStation>")
            End If
            builder.Append("<Status>" & item.Status & "</Status>")

            builder.Append("</Data>")
        Next

        Return builder.ToString()
    End Function

    Public Function GetRequestMixingStation(code As String, audit As AuditMessage) As ActionResult(Of RequestMixingStation) Implements IRequestMixingStationAdminService.GetRequestMixingStation
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        If audit Is Nothing Then
            Throw New ArgumentNullException("audit")
        End If
        Try
            Dim RequestMixingStation As RequestMixingStation = Me._RequestMixingStationRepository.GetRequestMixingStation(code.Trim())
            If RequestMixingStation IsNot Nothing AndAlso RequestMixingStation.Id > 0 Then
                Dim auditObject As New IndigoAuditSimpleEntity(Of RequestMixingStation)(RequestMixingStation, audit, Infrastructure.CrossCutting.Audit.Actions.Print)
                auditObject.Execute()
            End If

            Return New ActionResult(Of RequestMixingStation) With {.StateResult = True, .ObjectEmbbeded = RequestMixingStation}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RequestMixingStation) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function GetRequestMixingStationById(id As Integer) As ActionResult(Of RequestMixingStation) Implements IRequestMixingStationAdminService.GetRequestMixingStationById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Try
            Dim RequestMixingStation As RequestMixingStation = Me._RequestMixingStationRepository.GetRequestMixingStationById(id)
            Return New ActionResult(Of RequestMixingStation) With {.StateResult = True, .ObjectEmbbeded = RequestMixingStation}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RequestMixingStation) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Procesa la Solicitud de Central de Mezclas
    ''' </summary>
    ''' <param name="requestIds"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ProcessRequestMixingStation(requestIds As List(Of Integer), audit As AuditMessage) As ActionResult Implements IRequestMixingStationAdminService.ProcessRequestMixingStation
        Try
            If requestIds Is Nothing OrElse Not requestIds.Any() Then
                Throw New ArgumentNullException("RequestIds")
            End If

            Dim listRequestMixingStationDetail As List(Of RequestMixingStationDetail) = _requestMixingStationDetailRepository.GetRequestMixingStationDetailByListIds(requestIds)
            If listRequestMixingStationDetail Is Nothing Then
                Throw New ArgumentException("Solicitudes no encontrados")
            End If

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim uow As IUnitWork = Me._requestMixingStationDetailRepository.UnitWork
                listRequestMixingStationDetail.ForEach(Sub(ls As RequestMixingStationDetail)
                                                           With ls
                                                               ls.SendTo = 1
                                                               ls.ConfirmationUser = audit.CodeUser
                                                               ls.ConfirmationDate = DateTime.Now
                                                               ls.MarkAsModified
                                                           End With
                                                           Me._requestMixingStationDetailRepository.SaveEntity(ls)
                                                       End Sub)
                uow.Commit()
                scope.Complete()
            End Using
            Return New ActionResult With {.StateResult = True, .Message = String.Join(", ", listRequestMixingStationDetail)}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Public Function AnnulateRequests(requestMixingStationDetailIds As List(Of Integer)) As ActionResult Implements IRequestMixingStationAdminService.AnnulateRequests
        Try
            Dim requestDetails = _requestMixingStationDetailRepository.GetByFilter(Function(m) requestMixingStationDetailIds.Contains(m.Id), True, {"ConfirmationUnitDose"})

            requestDetails.ToList() _
                .ForEach(Sub(m)
                             m.Status = 3
                             m.ConfirmationUnitDose.ToList() _
                             .ForEach(Sub(o)
                                          Dim pharmaDoses = _pharmaDoseRepository.GetByFilter(Function(x) x.GroupingCodeDose = o.GroupingCodeDose)

                                          If pharmaDoses IsNot Nothing AndAlso pharmaDoses.Any() Then
                                              For Each pd In pharmaDoses
                                                  pd.DeliveryStatus = 3
                                              Next
                                          End If
                                      End Sub)
                         End Sub)

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                _requestMixingStationDetailRepository.UnitWork.Commit()
                scope.Complete()
            End Using

            Return New ActionResult With {.StateResult = True}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Reversa solicitudes activas al dashboard de confirmación de dosis unitaria.
    ''' </summary>
    ''' <param name="requestMixingStationDetailIds">Identificadores de los detalles de solicitud de central de mezclas a reversar.</param>
    ''' <param name="audit">Información de auditoría del usuario que ejecuta la acción.</param>
    ''' <returns>Resultado de la operación de reversa.</returns>
    Public Function ReverseRequestsToConfirmationUnitDose(requestMixingStationDetailIds As List(Of Integer), audit As AuditMessage) As ActionResult Implements IRequestMixingStationAdminService.ReverseRequestsToConfirmationUnitDose
        Return ReverseRequestsFromDashboard(requestMixingStationDetailIds, audit, False)
    End Function

    ''' <summary>
    ''' Devuelve solicitudes activas al flujo del servicio farmacéutico.
    ''' </summary>
    ''' <param name="requestMixingStationDetailIds">Identificadores de los detalles de solicitud de central de mezclas a devolver.</param>
    ''' <param name="audit">Información de auditoría del usuario que ejecuta la acción.</param>
    ''' <returns>Resultado de la operación de devolución al servicio farmacéutico.</returns>
    Public Function ReturnRequestsToPharmacy(requestMixingStationDetailIds As List(Of Integer), audit As AuditMessage) As ActionResult Implements IRequestMixingStationAdminService.ReturnRequestsToPharmacy
        Return ReverseRequestsFromDashboard(requestMixingStationDetailIds, audit, True)
    End Function

    ''' <summary>
    ''' Ejecuta la reversa de solicitudes del dashboard de central de mezclas hacia confirmación o farmacia,
    ''' restaurando el estado de la orden médica antes de desvincular la confirmación.
    ''' </summary>
    ''' <param name="requestMixingStationDetailIds">Identificadores de los detalles de solicitud de central de mezclas a reversar.</param>
    ''' <param name="audit">Información de auditoría del usuario que ejecuta la acción.</param>
    ''' <param name="returnToPharmacy">Indica si la reversa debe devolver la orden al servicio farmacéutico.</param>
    ''' <returns>Resultado de la operación de reversa.</returns>
    Private Function ReverseRequestsFromDashboard(requestMixingStationDetailIds As List(Of Integer), audit As AuditMessage, returnToPharmacy As Boolean) As ActionResult
        Try
            If requestMixingStationDetailIds Is Nothing OrElse Not requestMixingStationDetailIds.Any() Then
                Throw New ArgumentNullException(NameOf(requestMixingStationDetailIds))
            End If

            If audit Is Nothing Then
                Throw New ArgumentNullException(NameOf(audit))
            End If

            Dim ids = requestMixingStationDetailIds.Distinct().ToList()
            Dim requestDetails = _requestMixingStationDetailRepository _
                .GetByFilter(Function(m) ids.Contains(m.Id), True, {"ConfirmationUnitDose", "RequestMixingStationDetailPatients"}) _
                .ToList()

            Dim validation = ValidateDashboardReverse(ids, requestDetails, returnToPharmacy)
            If Not validation.StateResult Then
                Return validation
            End If

            Dim idPlaceholders = String.Join(",", ids.Select(Function(id, index) $"{{{index}}}"))
            Dim sendToParameterIndex = ids.Count
            Dim auditUserParameterIndex = ids.Count
            Dim auditDateParameterIndex = ids.Count + 1
            Dim operationDate = DateTime.Now
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim sendTo = If(returnToPharmacy, 1, 2)
                Dim routeParameters = ids.Cast(Of Object)().Concat({CObj(sendTo)}).ToArray()

                _requestMixingStationDetailRepository.ExecuteNonQuery($"
                UPDATE d SET
                    d.VIEPROCESSED = 0,
                    d.SENDTO = {{{sendToParameterIndex}}},
                    d.CodeSusceptibleMixingStation = CASE WHEN {{{sendToParameterIndex}}} = 1 THEN NULL ELSE d.CodeSusceptibleMixingStation END
                FROM dbo.HCFARMEPD d
                INNER JOIN MedicalHistory.PharmaDose pd ON pd.CodeSusceptibleMixingStation = d.CodeSusceptibleMixingStation
                INNER JOIN MixingStation.ConfirmationUnitDose cud ON cud.GroupingCodeDose = pd.GroupingCodeDose
                WHERE cud.RequestMixingStationDetailId IN ({idPlaceholders})", routeParameters)

                Dim auditParameters = ids.Cast(Of Object)().Concat({CObj(audit.CodeUser), CObj(operationDate)}).ToArray()

                _requestMixingStationDetailRepository.ExecuteNonQuery($"
                UPDATE MixingStation.ConfirmationUnitDose
                SET RequestMixingStationDetailId = NULL,
                    ModificationUser = {{{auditUserParameterIndex}}},
                    ModificationDate = {{{auditDateParameterIndex}}}
                WHERE RequestMixingStationDetailId IN ({idPlaceholders})", auditParameters)

                _requestMixingStationDetailRepository.ExecuteNonQuery($"
                UPDATE MixingStation.RequestMixingStationDetailPatients
                SET Status = 3,
                    AnnulmentUser = {{{auditUserParameterIndex}}},
                    AnnulmentDate = {{{auditDateParameterIndex}}}
                WHERE RequestMixingStationDetailId IN ({idPlaceholders})
                  AND Status <> 3", auditParameters)

                _requestMixingStationDetailRepository.ExecuteNonQuery($"
                UPDATE MixingStation.RequestMixingStationDetail
                SET Status = 3
                WHERE Id IN ({idPlaceholders})", ids.Cast(Of Object)().ToArray())

                scope.Complete()
            End Using

            Return New ActionResult With {
                .StateResult = True,
                .Message = If(returnToPharmacy,
                    "Solicitudes devueltas al Servicio Farmacéutico correctamente",
                    "Solicitudes reversadas a Confirmación de Dosis Unitaria correctamente")
            }
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Valida que las solicitudes seleccionadas puedan reversarse desde el dashboard de central de mezclas.
    ''' </summary>
    ''' <param name="ids">Identificadores solicitados para reversar.</param>
    ''' <param name="requestDetails">Detalles de solicitud recuperados desde el repositorio.</param>
    ''' <param name="returnToPharmacy">Indica si la validación corresponde al flujo de devolución a farmacia.</param>
    ''' <returns>Resultado de la validación de reversa.</returns>
    Private Function ValidateDashboardReverse(ids As List(Of Integer), requestDetails As List(Of RequestMixingStationDetail), returnToPharmacy As Boolean) As ActionResult
        If requestDetails.Count <> ids.Count Then
            Return New ActionResult With {.StateResult = False, .Message = "Una o más solicitudes seleccionadas no existen"}
        End If

        If requestDetails.Any(Function(m) m.CampaignDetailId.HasValue OrElse m.RequestMixingStationDetailPatients.Any(Function(p) p.CampaignDetailId.HasValue)) Then
            Return New ActionResult With {.StateResult = False, .Message = "No se pueden reversar solicitudes que ya se encuentran asociadas a campañas"}
        End If

        If requestDetails.Any(Function(m) m.EntityName <> NameOf(ConfirmationUnitDose) OrElse Not m.ConfirmationUnitDose.Any()) Then
            Return New ActionResult With {.StateResult = False, .Message = "Solo se pueden reversar solicitudes originadas desde Confirmación de Dosis Unitaria"}
        End If

        If requestDetails.Any(Function(m) m.Status = 3) Then
            Return New ActionResult With {.StateResult = False, .Message = "Existen solicitudes seleccionadas que ya se encuentran anuladas"}
        End If

        If requestDetails.Any(Function(m) m.SendTo <> 0) Then
            Return New ActionResult With {.StateResult = False, .Message = "Solo se pueden reversar solicitudes pendientes de procesamiento"}
        End If

        If returnToPharmacy AndAlso requestDetails.Any(Function(m) m.Source <> 1) Then
            Return New ActionResult With {.StateResult = False, .Message = "Solo las órdenes médicas pueden devolverse directamente al Servicio Farmacéutico"}
        End If

        Return New ActionResult With {.StateResult = True}
    End Function

    ''' <summary>
    ''' Funcion para vincular una readecuacion con el detalle de una solicitud
    ''' </summary>
    ''' <param name="RequestMSDetailId"></param>
    ''' <param name="Readjustment"></param>
    ''' <returns></returns>
    Public Function AssignReadjustmentToRequestMSDetail(RequestMSDetailId As Integer, Readjustment As Readjustments) As ActionResult Implements IRequestMixingStationAdminService.AssignReadjustmentToRequestMSDetail
        Dim unitOfWork As IUnitWork = Me._requestMixingStationDetailRepository.UnitWork
        If RequestMSDetailId = 0 Then
            Throw New ArgumentNullException("RequestMSDetailId")
        End If
        If Readjustment Is Nothing OrElse Readjustment?.Id = 0 OrElse Readjustment?.RequestPackageDetailStatusId = 0 Then
            Throw New ArgumentNullException("RequestPDStatusId")
        End If
        Dim RequestPDStatusId = Readjustment.RequestPackageDetailStatusId ''Readecuacion
        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                Dim _stringBuilder = New StringBuilder
                'se consultan la solicitud y el producto para nuevamente verificar que el match sea correcto
                Dim requestMixingStationDetail = Me._requestMixingStationDetailRepository.FirstOrDefault(Function(d) d.Id = RequestMSDetailId, True, {"Package.PackageDetail", "PackagePersonalized.PackagePersonalizedDetail", "RequestPackageDetailStatus"})
                Dim requestPackageDetailStatus = Me._requestPackageDetailStatusRepository.FirstOrDefault(Function(d) d.Id = RequestPDStatusId, True, {"Package.PackageDetail", "PackagePersonalized.PackagePersonalizedDetail"})

                If requestMixingStationDetail Is Nothing OrElse requestPackageDetailStatus Is Nothing Then
                    transaction.Dispose()
                    Return New ActionResult With {.StateResult = False, .Message = "La solicitud o el Producto a guardar no existen"}
                End If

                If requestMixingStationDetail.PackageId <> requestPackageDetailStatus.PackageId Then
                    _stringBuilder.AppendLine("El paquete estandar no son iguales")
                End If

                If If(requestMixingStationDetail.PackagePersonalizedId Is Nothing, requestMixingStationDetail.Package.Concentration, requestMixingStationDetail.PackagePersonalized.Concentration) _
                                                                    <>
                   If(requestPackageDetailStatus.PackagePersonalizedId Is Nothing, requestPackageDetailStatus.Package.Concentration, requestPackageDetailStatus.PackagePersonalized.Concentration) Then

                    _stringBuilder.AppendLine("La Concentración de la solictud no es igual a la de la readecuación")

                End If

                If If(requestMixingStationDetail.PackagePersonalizedId Is Nothing, requestMixingStationDetail.Package.ConcentrationMeasurementUnitId, requestMixingStationDetail.PackagePersonalized.ConcentrationMeasurementUnitId) _
                                                                     <>
                    If(requestPackageDetailStatus.PackagePersonalizedId Is Nothing, requestPackageDetailStatus.Package.ConcentrationMeasurementUnitId, requestPackageDetailStatus.PackagePersonalized.ConcentrationMeasurementUnitId) Then

                    _stringBuilder.AppendLine("La Unidad de medida de la concentración de la solictud no es igual al de la readecuación")

                End If

                If If(requestMixingStationDetail.PackagePersonalizedId Is Nothing, requestMixingStationDetail.Package.VolumeTotalOrder, requestMixingStationDetail.PackagePersonalized.VolumeTotalOrder) _
                                                        <>
                    If(requestPackageDetailStatus.PackagePersonalizedId Is Nothing, requestPackageDetailStatus.Package.VolumeTotalOrder, requestPackageDetailStatus.PackagePersonalized.VolumeTotalOrder) Then

                    _stringBuilder.AppendLine("El volumen de la solicitud es diferente al de la readecuación")

                End If

                If If(requestMixingStationDetail.PackagePersonalizedId Is Nothing, requestMixingStationDetail.Package.VolumeTotalOrderMeasurementUnitId, requestMixingStationDetail.PackagePersonalized.VolumeTotalOrderMeasurementUnitId) _
                                                                        <>
                   If(requestPackageDetailStatus.PackagePersonalizedId Is Nothing, requestPackageDetailStatus.Package.VolumeTotalOrderMeasurementUnitId, requestPackageDetailStatus.PackagePersonalized.VolumeTotalOrderMeasurementUnitId) Then

                    _stringBuilder.AppendLine("La unidad de medida del volumen de la solicitud es diferente al de la readecuación")

                End If

                If If(requestMixingStationDetail.PackagePersonalizedId Is Nothing,
                   Not requestMixingStationDetail.Package.PackageDetail.Any(Function(x) x.MainMedicine AndAlso x.AtcId = If(requestPackageDetailStatus.PackagePersonalizedId Is Nothing, requestPackageDetailStatus.Package.PackageDetail.FirstOrDefault(Function(s) s.MainMedicine).AtcId, requestPackageDetailStatus.PackagePersonalized.PackagePersonalizedDetail.FirstOrDefault(Function(s) s.MainMedicine).AtcId)),
                    Not requestMixingStationDetail.PackagePersonalized.PackagePersonalizedDetail.Any(Function(x) x.MainMedicine AndAlso x.AtcId = If(requestPackageDetailStatus.PackagePersonalizedId Is Nothing, requestPackageDetailStatus.Package.PackageDetail.FirstOrDefault(Function(s) s.MainMedicine).AtcId, requestPackageDetailStatus.PackagePersonalized.PackagePersonalizedDetail.FirstOrDefault(Function(s) s.MainMedicine).AtcId))) Then

                    _stringBuilder.AppendLine("El medicamento Principal de la solicitud es diferente al de la readecuación")

                End If

                If If(requestMixingStationDetail.PackagePersonalizedId Is Nothing,
                                            Not requestMixingStationDetail.Package.PackageDetail.Any(Function(x) x.Vehicle AndAlso x.AtcId = If(requestPackageDetailStatus.PackagePersonalizedId Is Nothing, requestPackageDetailStatus.Package.PackageDetail.FirstOrDefault(Function(s) s.Vehicle).AtcId, requestPackageDetailStatus.PackagePersonalized.PackagePersonalizedDetail.FirstOrDefault(Function(s) s.Vehicle).AtcId)),
                                            Not requestMixingStationDetail.PackagePersonalized.PackagePersonalizedDetail.Any(Function(x) x.Vehicle AndAlso x.AtcId = If(requestPackageDetailStatus.PackagePersonalizedId Is Nothing, requestPackageDetailStatus.Package.PackageDetail.FirstOrDefault(Function(s) s.Vehicle).AtcId, requestPackageDetailStatus.PackagePersonalized.PackagePersonalizedDetail.FirstOrDefault(Function(s) s.Vehicle).AtcId))) Then

                    _stringBuilder.AppendLine("El Vehiculo de la solicitud es diferente al de la readecuación")

                End If

                If _stringBuilder.Length > 0 Then
                    transaction.Dispose()
                    Return New ActionResult With {.StateResult = False, .Message = $"No Pasó las siguientes validaciones :{_stringBuilder.ToString}"}
                End If

                Dim _readjustment = Me._readjustmentsRepository.FirstOrDefault(Function(x) x.Id = Readjustment.Id, True)
                If Readjustment Is Nothing OrElse Readjustment?.Id = 0 Then
                    Throw New ArgumentException("Readjustment")
                End If

                For Each item In requestMixingStationDetail.RequestPackageDetailStatus
                    _requestPackageDetailStatusRepository.DeleteEntity(item)
                Next

                _readjustment.Status = 3 ''Enlazada a una Solicitud Pre existente
                requestPackageDetailStatus.Status = 2 '' Producto Terminado
                requestPackageDetailStatus.SendTo = 0 '' No generado orden de translado
                requestPackageDetailStatus.GroupingCodeDose = requestMixingStationDetail.RequestPackageDetailStatus(0).GroupingCodeDose ''codigo de dosis inicial
                ''Se asigna la solicitud de la readecuacion
                requestPackageDetailStatus.RequestMixingStationDetailId = RequestMSDetailId
                requestMixingStationDetail.RequestPackageDetailStatusId = RequestPDStatusId

                Me._readjustmentsRepository.SaveEntity(_readjustment)
                Me._requestMixingStationDetailRepository.SaveEntity(requestMixingStationDetail)
                _readjustmentLogAdminService.AddAdjustmentLog(requestPackageDetailStatus.RequestMixingStationDetailId, requestPackageDetailStatus.Id, requestPackageDetailStatus.BatchCode, audit:=Nothing)
                Me._requestPackageDetailStatusRepository.UnitWork.Commit()

                unitOfWork.Commit()
                transaction.Complete()

                Return New ActionResult With {.StateResult = True, .Message = "Se vinculó la readecuación al detalle de la solicitud"}
            Catch Null As ArgumentNullException
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(Null)}
            Catch ex As Exception
                unitOfWork.RollbackChanges()
                transaction.Dispose()
                Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End Try
        End Using
    End Function


#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: elimine el estado administrado (objetos administrados).
            End If
            _RequestMixingStationRepository = Nothing
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
