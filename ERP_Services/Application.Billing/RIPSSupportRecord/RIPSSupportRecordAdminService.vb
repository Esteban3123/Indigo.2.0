Imports System.Threading.Tasks
Imports Domain.Entities
Imports Domain.Crystal
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports System.Text
Imports System.Collections.Concurrent
Imports System.Dynamic
Imports System.Transactions
Imports Domain.Crystal.Entities
Imports Domain.Payroll
Imports Domain.Entities.Service
Imports Domain.Base



Public Class RIPSSupportRecordAdminService
    Implements IRIPSSupportRecordAdminService

#Region "Enumeraciones y Constantes"

    ''' <summary>
    ''' Escenarios posibles para el procesamiento de RIPSSupportRecord
    ''' </summary>
    Private Enum ERIPSSupportRecordScenario
        ''' <summary>
        ''' Crear nuevo registro (Id = 0, Status = 0)
        ''' </summary>
        Create = 1
        ''' <summary>
        ''' Actualizar registro existente (Id > 0, Status = 0)
        ''' </summary>
        Update = 2
        ''' <summary>
        ''' Anular registro existente (Status = 3)
        ''' </summary>
        Annul = 3
    End Enum

    ''' <summary>
    ''' Estados del RIPSSupportRecord
    ''' </summary>
    Private Class RIPSSupportRecordStatus
        Public Const Registered As Byte = 0
        Public Const Confirmed As Byte = 1
        Public Const Annulled As Byte = 3
    End Class

#End Region

    Private ReadOnly _RIPSSupportRecordRepository As IRIPSSupportRecordRepository
    Private ReadOnly _sequenceDRepository As IBillingSequenceDetailRepository
    Private ReadOnly _serviceOrderRepository As IServiceOrderRepository
    Private ReadOnly _serviceOrderAdminService As IServiceOrderAdminService
    Private ReadOnly _patientRepository As IPatientRepository
    Private ReadOnly _careGroupRepository As ICareGroupRepository
    Private ReadOnly _admissionRepository As IAdmissionRepository
    Private ReadOnly _healthAdministratorRepository As IHealthAdministratorRepository
    Private ReadOnly _cupsEntityRepository As ICupsEntityRepository
    Private ReadOnly _serviceOrderDetailAdminService As IServiceOrderDetailAdminService
    Private ReadOnly _thirdPartyRepository As IThirdPartyRepository
    Private ReadOnly _functionalUnitRepository As IFunctionalUnitRepository
    Private ReadOnly _specialityRepository As ISpecialityRepository
    Private ReadOnly _contractServices As IContractServices
    Private ReadOnly _cupsEntityContractDescriptionsRepository As ICupsEntityContractDescriptionsRepository

    Public Sub New(ByVal rIPSSupportRecordRepository As IRIPSSupportRecordRepository,
                   ByVal sequenceDRepository As IBillingSequenceDetailRepository,
                   ByVal serviceOrderRepository As IServiceOrderRepository,
                   ByVal serviceOrderAdminService As IServiceOrderAdminService,
                   ByVal patientRepository As IPatientRepository,
                   ByVal careGroupRepository As ICareGroupRepository,
                   ByVal admissionRepository As IAdmissionRepository,
                   ByVal healthAdministratorRepository As IHealthAdministratorRepository,
                   ByVal cupsEntityRepository As ICupsEntityRepository,
                   ByVal serviceOrderDetailAdminService As IServiceOrderDetailAdminService,
                   ByVal thirdPartyRepository As IThirdPartyRepository,
                   ByVal functionalUnitRepository As IFunctionalUnitRepository,
                   ByVal specialityRepository As ISpecialityRepository,
                   ByVal contractServices As IContractServices,
                   ByVal cupsEntityContractDescriptionsRepository As ICupsEntityContractDescriptionsRepository)
        _RIPSSupportRecordRepository = rIPSSupportRecordRepository
        _sequenceDRepository = sequenceDRepository
        _serviceOrderRepository = serviceOrderRepository
        _serviceOrderAdminService = serviceOrderAdminService
        _patientRepository = patientRepository
        _careGroupRepository = careGroupRepository
        _admissionRepository = admissionRepository
        _healthAdministratorRepository = healthAdministratorRepository
        _cupsEntityRepository = cupsEntityRepository
        _serviceOrderDetailAdminService = serviceOrderDetailAdminService
        _thirdPartyRepository = thirdPartyRepository
        _functionalUnitRepository = functionalUnitRepository
        _specialityRepository = specialityRepository
        _contractServices = contractServices
        _cupsEntityContractDescriptionsRepository = cupsEntityContractDescriptionsRepository
    End Sub

    Public Async Function GetRIPSSupportRecordByIdAsync(ByVal id As Integer) As Task(Of RIPSSupportRecord) Implements IRIPSSupportRecordAdminService.GetRIPSSupportRecordByIdAsync
        Try
            Return Await _RIPSSupportRecordRepository.GetRIPSSupportRecordByIdAsync(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    Public Async Function GetRIPSSupportRecordByCodeAsync(ByVal code As String) As Task(Of RIPSSupportRecord) Implements IRIPSSupportRecordAdminService.GetRIPSSupportRecordByCodeAsync
        Try
            Dim ripsRecord = Await _RIPSSupportRecordRepository.GetRIPSSupportRecordByCodeAsync(code)
            Return ripsRecord
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

#Region "Determinación de Escenario"

    ''' <summary>
    ''' Determina el escenario de procesamiento basándose en el Status e Id del RIPSSupportRecord
    ''' </summary>
    ''' <param name="supportRecord">Registro de soporte RIPS</param>
    ''' <returns>El escenario correspondiente</returns>
    Private Function DetermineScenario(ByVal supportRecord As RIPSSupportRecord) As ERIPSSupportRecordScenario
        ' Escenario de Anulación: Status = 3
        If supportRecord.Status = RIPSSupportRecordStatus.Annulled Then
            Return ERIPSSupportRecordScenario.Annul
        End If

        ' Escenario de Actualización: Id > 0 y Status = 0 (Registrado)
        If supportRecord.Id > 0 AndAlso supportRecord.Status = RIPSSupportRecordStatus.Registered Then
            Return ERIPSSupportRecordScenario.Update
        End If

        ' Escenario de Creación: Id = 0 y Status = 0 (Registrado)
        Return ERIPSSupportRecordScenario.Create
    End Function

    ''' <summary>
    ''' Valida si el escenario es válido para el registro actual
    ''' </summary>
    ''' <param name="supportRecord">Registro de soporte RIPS</param>
    ''' <param name="scenario">Escenario determinado</param>
    ''' <returns>ActionResult con error si la validación falla, Nothing si es exitosa</returns>
    Private Function ValidateScenario(ByVal supportRecord As RIPSSupportRecord, ByVal scenario As ERIPSSupportRecordScenario) As ActionResult(Of RIPSSupportRecord)
        Select Case scenario
            Case ERIPSSupportRecordScenario.Update
                ' Para actualización, debe existir ServiceOrderId válido
                If Not supportRecord.ServiceOrderId.HasValue OrElse supportRecord.ServiceOrderId.Value <= 0 Then
                    Return New ActionResult(Of RIPSSupportRecord) With {
                        .StatusCode = eStatusResult.WARNING,
                        .StateResult = False,
                        .Message = "No se puede actualizar el registro de soporte RIPS porque no tiene una orden de servicio asociada"
                    }
                End If

        End Select

        Return Nothing
    End Function

    ''' <summary>
    ''' Valida si el registro de soporte RIPS puede ser modificado basándose en su estado actual.
    ''' Los registros confirmados (Status = 1) no pueden ser modificados, solo anulados.
    ''' </summary>
    ''' <param name="id">Identificador del registro a validar</param>
    ''' <returns>ActionResult con error si no puede ser modificado, Nothing si puede modificarse</returns>
    Private Async Function ValidateRecordCanBeModifiedAsync(ByVal id As Integer) As Task(Of ActionResult(Of RIPSSupportRecord))
        If id <= 0 Then
            Return Nothing ' Registro nuevo, no necesita validación
        End If

        Dim recordStatus As Tuple(Of Byte, String) = Await _RIPSSupportRecordRepository.GetRIPSSupportRecordStatusByIdAsync(id)

        If recordStatus Is Nothing Then
            Return New ActionResult(Of RIPSSupportRecord) With {
                .StatusCode = eStatusResult.WARNING,
                .StateResult = False,
                .Message = String.Format("No se encontró el registro de soporte RIPS con Id {0}", id)
            }
        End If

        Dim status As Byte = recordStatus.Item1
        Dim code As String = recordStatus.Item2

        ' Validar que el registro no esté confirmado
        If status = RIPSSupportRecordStatus.Confirmed Then
            Return New ActionResult(Of RIPSSupportRecord) With {
                .StatusCode = eStatusResult.WARNING,
                .StateResult = False,
                .Message = String.Format("El registro de soporte RIPS '{0}' se encuentra confirmado y no puede ser modificado. Para realizar cambios, debe anular este registro y crear uno nuevo.", code)
            }
        End If

        ' Validar que el registro no esté anulado
        If status = RIPSSupportRecordStatus.Annulled Then
            Return New ActionResult(Of RIPSSupportRecord) With {
                .StatusCode = eStatusResult.WARNING,
                .StateResult = False,
                .Message = String.Format("El registro de soporte RIPS '{0}' se encuentra anulado y no puede ser modificado.", code)
            }
        End If

        Return Nothing ' El registro puede ser modificado
    End Function

#End Region

#Region "Preparación de ServiceOrder por Escenario"

    ''' <summary>
    ''' Prepara la ServiceOrder para el escenario de actualización.
    ''' Obtiene la orden existente, marca todos los detalles como Deleted y genera nuevos detalles como Added.
    ''' </summary>
    ''' <param name="supportRecord">Registro de soporte RIPS con los detalles actualizados</param>
    ''' <param name="careGroup">Grupo de atención</param>
    ''' <param name="operatingUnitId">Identificador de la unidad operativa</param>
    ''' <param name="args">Argumentos con los detalles para generar</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <returns>ActionResult con la ServiceOrder preparada para actualización</returns>
    Private Function PrepareServiceOrderForUpdate(
        ByVal supportRecord As RIPSSupportRecord,
        ByVal careGroup As CareGroup,
        ByVal operatingUnitId As Integer,
        ByVal args As Object,
        ByVal audit As AuditMessage) As ActionResult(Of ServiceOrder)

        Try
            ' Obtener la ServiceOrder existente con sus detalles
            Dim existingServiceOrder As ServiceOrder = _serviceOrderRepository.GetServiceOrderById(supportRecord.ServiceOrderId.Value)

            If existingServiceOrder Is Nothing Then
                Return New ActionResult(Of ServiceOrder) With {
                    .StatusCode = eStatusResult.WARNING,
                    .StateResult = False,
                    .Message = String.Format("No se encontró la orden de servicio con Id {0}", supportRecord.ServiceOrderId.Value)
                }
            End If

            ' Iniciar tracking en la orden de servicio existente
            existingServiceOrder.StartTracking()

            ' Marcar TODOS los detalles existentes como Deleted
            For Each existingDetail As ServiceOrderDetail In existingServiceOrder.ServiceOrderDetail.ToList()
                existingDetail.StartTracking()
                existingDetail.MarkAsDeleted()
            Next

            ' Generar los NUEVOS detalles basados en los RIPSSupportRecordDetail actuales
            Dim resultNewDetails = GenerateNewServiceOrderDetails(existingServiceOrder, careGroup, operatingUnitId, args, audit)

            If Not resultNewDetails.StateResult Then
                Return New ActionResult(Of ServiceOrder) With {
                    .StatusCode = eStatusResult.WARNING,
                    .StateResult = False,
                    .Message = resultNewDetails.Message
                }
            End If

            ' Agregar los nuevos detalles a la orden de servicio
            For Each newDetail As ServiceOrderDetail In resultNewDetails.ObjectEmbbeded
                ' Asegurar que el nuevo detalle esté marcado como Added
                newDetail.StartTracking()
                newDetail.MarkAsAdded()
                existingServiceOrder.ServiceOrderDetail.Add(newDetail)
            Next

            existingServiceOrder.Status = 1

            ' Evitar tracking duplicado
            If existingServiceOrder.RIPSSupportRecord IsNot Nothing Then
                existingServiceOrder.RIPSSupportRecord.Clear()
            End If

            Return New ActionResult(Of ServiceOrder) With {
                .StatusCode = eStatusResult.SUCCESS,
                .StateResult = True,
                .ObjectEmbbeded = existingServiceOrder
            }

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ServiceOrder) With {
                .StatusCode = eStatusResult.WARNING,
                .StateResult = False,
                .Message = String.Format("Error al preparar la orden de servicio para actualización: {0}", IndigoManagementExceptions.GetExceptionDetails(ex))
            }
        End Try
    End Function

    ''' <summary>
    ''' Prepara la ServiceOrder para el escenario de anulación.
    ''' Obtiene la orden existente y cambia su Status a 3 (Anulado).
    ''' </summary>
    ''' <param name="supportRecord">Registro de soporte RIPS a anular</param>
    ''' <returns>ActionResult con la ServiceOrder preparada para anulación</returns>
    Private Function PrepareServiceOrderForAnnulment(ByVal supportRecord As RIPSSupportRecord) As ActionResult(Of ServiceOrder)
        Try
            ' Obtener la ServiceOrder existente con sus detalles
            Dim existingServiceOrder As ServiceOrder = _serviceOrderRepository.GetServiceOrderById(supportRecord.ServiceOrderId.Value)

            If existingServiceOrder Is Nothing Then
                Return New ActionResult(Of ServiceOrder) With {
                    .StatusCode = eStatusResult.WARNING,
                    .StateResult = False,
                    .Message = String.Format("No se encontró la orden de servicio con Id {0}", supportRecord.ServiceOrderId.Value)
                }
            End If

            existingServiceOrder.StartTracking()
            existingServiceOrder.Status = 3 ' Anulado

            ' Evitar tracking duplicado
            If existingServiceOrder.RIPSSupportRecord IsNot Nothing Then
                existingServiceOrder.RIPSSupportRecord.Clear()
            End If

            Return New ActionResult(Of ServiceOrder) With {
                .StatusCode = eStatusResult.SUCCESS,
                .StateResult = True,
                .ObjectEmbbeded = existingServiceOrder
            }

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ServiceOrder) With {
                .StatusCode = eStatusResult.WARNING,
                .StateResult = False,
                .Message = String.Format("Error al preparar la orden de servicio para anulación: {0}", IndigoManagementExceptions.GetExceptionDetails(ex))
            }
        End Try
    End Function

    ''' <summary>
    ''' Genera los nuevos detalles de ServiceOrder basándose en los detalles de RIPSSupportRecord.
    ''' Este método es utilizado tanto para creación como para actualización.
    ''' </summary>
    ''' <param name="serviceOrder">Orden de servicio (nueva o existente)</param>
    ''' <param name="careGroup">Grupo de atención</param>
    ''' <param name="operatingUnitId">Identificador de la unidad operativa</param>
    ''' <param name="args">Argumentos con los detalles a procesar</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <returns>ActionResult con la lista de ServiceOrderDetail generados</returns>
    Private Function GenerateNewServiceOrderDetails(
        ByVal serviceOrder As ServiceOrder,
        ByVal careGroup As CareGroup,
        ByVal operatingUnitId As Integer,
        ByVal args As Object,
        ByVal audit As AuditMessage) As ActionResult(Of List(Of ServiceOrderDetail))

        Try
            ' Obtener paciente
            Dim patient As INPACIENT = _patientRepository.GetOnlyPatientByIdentification(args.PatientCode.ToString().Trim(), False)
            If patient Is Nothing Then
                Return New ActionResult(Of List(Of ServiceOrderDetail)) With {
                    .StatusCode = eStatusResult.WARNING,
                    .StateResult = False,
                    .Message = String.Format("No se encontró el paciente con código {0}", args.PatientCode)
                }
            End If

            Dim ThirdPartyPatientId As Integer = GetThirdPartyPatientId(patient)

            ' Obtener homologaciones de CUPS
            Dim resultHomologation As ActionResult(Of List(Of List(Of CupsHomologation))) = GetHomologationsCups(CType(args.Details, List(Of Object)), careGroup.Id, False)
            If Not resultHomologation.StateResult Then
                Return New ActionResult(Of List(Of ServiceOrderDetail)) With {
                    .StatusCode = eStatusResult.WARNING,
                    .StateResult = False,
                    .Message = resultHomologation.Message
                }
            End If

            Dim homologations As List(Of List(Of CupsHomologation)) = resultHomologation.ObjectEmbbeded
            Dim careCenterCode As String = CType(args.CareCenterCode, String).Trim()
            Dim newDetails As New List(Of ServiceOrderDetail)()
            Dim Identity As Integer = 0

            ' Procesar cada detalle
            For Each detail As Object In CType(args.Details, List(Of Object)).ToList()
                Identity += 1

                Dim resultDetail = ProcessServiceOrderDetail(
                    detail,
                    serviceOrder,
                    careGroup,
                    homologations,
                    patient,
                    careCenterCode,
                    ThirdPartyPatientId,
                    Identity,
                    args.AuthorizationNumber.ToString())

                If Not resultDetail.StateResult Then
                    Return New ActionResult(Of List(Of ServiceOrderDetail)) With {
                        .StatusCode = eStatusResult.WARNING,
                        .StateResult = False,
                        .Message = resultDetail.Message
                    }
                End If

                newDetails.Add(resultDetail.ObjectEmbbeded)
            Next

            Return New ActionResult(Of List(Of ServiceOrderDetail)) With {
                .StatusCode = eStatusResult.SUCCESS,
                .StateResult = True,
                .ObjectEmbbeded = newDetails
            }

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of ServiceOrderDetail)) With {
                .StatusCode = eStatusResult.WARNING,
                .StateResult = False,
                .Message = String.Format("Error al generar los detalles de la orden de servicio: {0}", IndigoManagementExceptions.GetExceptionDetails(ex))
            }
        End Try
    End Function

#End Region

    Public Async Function NewRIPSSupportRecordAsync(ByVal supportRecord As RIPSSupportRecord, ByVal operatingUnitId As Integer, ByVal audit As AuditMessage, Optional ByVal idSequence As Long = 0) As Task(Of ActionResult(Of RIPSSupportRecord)) Implements IRIPSSupportRecordAdminService.NewRIPSSupportRecordAsync
        Try
            ' Validación de parámetros de entrada
            Dim validationResult = ValidateNewRIPSSupportRecordInput(supportRecord, operatingUnitId, audit)
            If validationResult IsNot Nothing Then
                Return validationResult
            End If

            ' PASO 1: Determinar el escenario de procesamiento
            Dim scenario As ERIPSSupportRecordScenario = DetermineScenario(supportRecord)

            ' PASO 2: Validar que el escenario sea válido
            Dim scenarioValidation = ValidateScenario(supportRecord, scenario)
            If scenarioValidation IsNot Nothing Then
                Return scenarioValidation
            End If

            ' PASO 3: Ejecutar según el escenario
            Select Case scenario
                Case ERIPSSupportRecordScenario.Create
                    Return Await ProcessCreateScenarioAsync(supportRecord, operatingUnitId, audit, idSequence)

                Case ERIPSSupportRecordScenario.Update
                    Return Await ProcessUpdateScenarioAsync(supportRecord, operatingUnitId, audit)

                Case ERIPSSupportRecordScenario.Annul
                    Return Await ProcessAnnulScenarioAsync(supportRecord, audit)

                Case Else
                    Return New ActionResult(Of RIPSSupportRecord) With {
                        .StatusCode = eStatusResult.WARNING,
                        .StateResult = False,
                        .Message = "Escenario de procesamiento no reconocido"
                    }
            End Select

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RIPSSupportRecord) With {
                .StatusCode = eStatusResult.EXCEPTION,
                .StateResult = False,
                .Message = String.Format("Error inesperado al procesar el soporte RIPS: {0}", IndigoManagementExceptions.GetExceptionDetails(ex))
            }
        End Try
    End Function

#Region "Procesamiento por Escenario"

    ''' <summary>
    ''' Procesa el escenario de creación de un nuevo RIPSSupportRecord y su ServiceOrder asociada
    ''' </summary>
    ''' <param name="supportRecord">Registro de soporte RIPS a crear</param>
    ''' <param name="operatingUnitId">Identificador de la unidad operativa</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <param name="idSequence">Identificador de la secuencia numérica para generar el código</param>
    ''' <returns>ActionResult con el registro creado o mensaje de error</returns>
    Private Async Function ProcessCreateScenarioAsync(
        ByVal supportRecord As RIPSSupportRecord,
        ByVal operatingUnitId As Integer,
        ByVal audit As AuditMessage,
        ByVal idSequence As Long) As Task(Of ActionResult(Of RIPSSupportRecord))

        Try
            ' Preparar los detalles para la orden de servicio
            Dim prepareResult = PrepareServiceOrderDetails(supportRecord)
            If Not prepareResult.StateResult Then
                Return New ActionResult(Of RIPSSupportRecord) With {
                    .StatusCode = eStatusResult.WARNING,
                    .StateResult = False,
                    .Message = prepareResult.Message
                }
            End If

            Dim detailsList As List(Of Object) = prepareResult.ObjectEmbbeded
            Dim admission = _admissionRepository.GetAdmissionByCode(supportRecord.AdmissionNumber)

            If admission Is Nothing Then
                Return New ActionResult(Of RIPSSupportRecord) With {
                    .StatusCode = eStatusResult.WARNING,
                    .StateResult = False,
                    .Message = "No se encontró el ingreso asociado al soporte RIPS"
                }
            End If

            ' Crear objeto args
            Dim args As Object = CreateServiceOrderArgs(admission, detailsList)

            Dim careGroup As CareGroup = _careGroupRepository.GetCareGroupById(args.CareGroupId)

            If careGroup Is Nothing OrElse careGroup.Id <= 0 Then
                Return New ActionResult(Of RIPSSupportRecord) With {
                    .StatusCode = eStatusResult.WARNING,
                    .StateResult = False,
                    .Message = "No se encontró el grupo de atención asociado al soporte RIPS"
                }
            End If

            ' Validar contrato si es EAPB
            Dim contractValidationResult = ValidateContractForCareGroup(careGroup)
            If Not contractValidationResult.StateResult Then
                Return New ActionResult(Of RIPSSupportRecord) With {
                    .StatusCode = eStatusResult.WARNING,
                    .StateResult = False,
                    .Message = contractValidationResult.Message
                }
            End If

            Dim messageNotificationContract As String = If(contractValidationResult.MessageResult IsNot Nothing AndAlso contractValidationResult.MessageResult.Count > 0,
                                                           String.Join(vbCrLf, contractValidationResult.MessageResult.ToArray()),
                                                           String.Empty)

            ' Transacción
            Using transactionScope As New TransactionScope(TransactionScopeOption.RequiresNew,
                                                           New TransactionOptions() With {
                                                               .Timeout = TransactionManager.MaximumTimeout,
                                                               .IsolationLevel = IsolationLevel.ReadCommitted
                                                           },
                                                           TransactionScopeAsyncFlowOption.Enabled)
                Try
                    ' PASO 1: Generar código con secuencia numérica
                    Dim sequenceUnitOfWork As IUnitWork = _sequenceDRepository.UnitWork
                    Dim seq As BillingSequenceDetail = _sequenceDRepository.GetSequenseDById(CInt(idSequence))

                    If seq IsNot Nothing AndAlso seq.Id > 0 AndAlso seq.BillingSequence IsNot Nothing AndAlso seq.BillingSequence.Sequential Then
                        Dim generatedCode As String = Infrastructure.CrossCutting.Base.Sequense.GetSequense(seq.Sequense.Pattern, seq.Next)

                        If generatedCode IsNot Nothing AndAlso Not generatedCode.Equals(Infrastructure.CrossCutting.Base.Sequense.ERROR_MAXVALUE) Then
                            supportRecord.Code = generatedCode
                            seq.Next += 1
                            _sequenceDRepository.SaveEntity(seq)
                        Else
                            Return New ActionResult(Of RIPSSupportRecord) With {
                                .StatusCode = eStatusResult.WARNING,
                                .StateResult = False,
                                .MessageResult = {"_Seq02_"}.ToList(),
                                .Message = "La secuencia numérica para Registro de Soporte RIPS alcanzó su valor máximo."
                            }
                        End If
                    Else
                        Return New ActionResult(Of RIPSSupportRecord) With {
                            .StatusCode = eStatusResult.WARNING,
                            .StateResult = False,
                            .MessageResult = {"_Seq02_"}.ToList(),
                            .Message = "No se encontró la secuencia numérica para Registro de Soporte RIPS o no está configurada como secuencial."
                        }
                    End If

                    ' PASO 2: Guardar el RIPSSupportRecord con el código generado
                    supportRecord.CreationUser = audit.CodeUser
                    supportRecord.CreationDate = Date.Now
                    _RIPSSupportRecordRepository.SaveEntity(supportRecord)
                    _RIPSSupportRecordRepository.UnitWork.Commit()
                    sequenceUnitOfWork.Commit()

                    ' PASO 2: Generar ServiceOrder nueva con EntityId y EntityCode del RIPSSupportRecord guardado
                    Dim resultServiceOrder As ActionResult(Of ServiceOrder) = GenerateServiceOrder(careGroup, operatingUnitId, args, audit, supportRecord.Id, If(String.IsNullOrWhiteSpace(supportRecord.Code), String.Empty, supportRecord.Code))

                    If Not resultServiceOrder.StateResult OrElse resultServiceOrder.ObjectEmbbeded Is Nothing Then
                        Return New ActionResult(Of RIPSSupportRecord) With {
                            .StatusCode = eStatusResult.WARNING,
                            .StateResult = False,
                            .Message = If(resultServiceOrder.Message, "No se pudo generar la orden de servicio")
                        }
                    End If

                    ' PASO 3: Guardar ServiceOrder
                    Dim _idCurrentSequence As Integer = 0
                    Dim resultSaveServiceOrder = _serviceOrderAdminService.SaveServiceOrder(resultServiceOrder.ObjectEmbbeded, audit, _idCurrentSequence)

                    If resultSaveServiceOrder Is Nothing OrElse Not resultSaveServiceOrder.StateResult Then
                        Return New ActionResult(Of RIPSSupportRecord) With {
                            .StatusCode = eStatusResult.WARNING,
                            .StateResult = False,
                            .Message = If(resultSaveServiceOrder?.Message, "Error inesperado al guardar la orden de servicio")
                        }
                    End If

                    Dim savedServiceOrder As ServiceOrder = resultSaveServiceOrder.ObjectEmbbeded

                    ' PASO 4: Actualizar RIPSSupportRecord con el ServiceOrderId
                    supportRecord.ServiceOrderId = savedServiceOrder.Id
                    _RIPSSupportRecordRepository.SaveEntity(supportRecord)
                    _RIPSSupportRecordRepository.UnitWork.Commit()

                    transactionScope.Complete()

                    Dim successMessage As String = String.Format("Registro de soporte RIPS creado exitosamente. Orden de servicio generada (Código: {0}). Registro RIPS (Código: {1}).",
                                                                 If(savedServiceOrder?.Code, "N/A"),
                                                                 If(supportRecord?.Code, "N/A"))

                    If Not String.IsNullOrEmpty(messageNotificationContract) Then
                        successMessage = String.Concat(successMessage, vbCrLf, messageNotificationContract)
                    End If

                    Return New ActionResult(Of RIPSSupportRecord) With {
                        .StatusCode = eStatusResult.SUCCESS,
                        .StateResult = True,
                        .ObjectEmbbeded = supportRecord,
                        .Message = successMessage
                    }

                Catch ex As Exception
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                    Return New ActionResult(Of RIPSSupportRecord) With {
                        .StatusCode = eStatusResult.WARNING,
                        .StateResult = False,
                        .Message = String.Format("Error al crear el soporte RIPS: {0}", IndigoManagementExceptions.GetExceptionDetails(ex))
                    }
                End Try
            End Using

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RIPSSupportRecord) With {
                .StatusCode = eStatusResult.EXCEPTION,
                .StateResult = False,
                .Message = String.Format("Error inesperado en el escenario de creación: {0}", IndigoManagementExceptions.GetExceptionDetails(ex))
            }
        End Try
    End Function

    ''' <summary>
    ''' Procesa el escenario de actualización de un RIPSSupportRecord existente.
    ''' Elimina todos los ServiceOrderDetail existentes y genera nuevos basados en los detalles actualizados.
    ''' Los registros confirmados no pueden ser actualizados, solo anulados.
    ''' </summary>
    Private Async Function ProcessUpdateScenarioAsync(
        ByVal supportRecord As RIPSSupportRecord,
        ByVal operatingUnitId As Integer,
        ByVal audit As AuditMessage) As Task(Of ActionResult(Of RIPSSupportRecord))

        Try
            ' Validar que el registro puede ser modificado (no está confirmado ni anulado)
            Dim canModifyResult = Await ValidateRecordCanBeModifiedAsync(supportRecord.Id)
            If canModifyResult IsNot Nothing Then
                Return canModifyResult
            End If

            ' Preparar los detalles para la orden de servicio
            Dim prepareResult = PrepareServiceOrderDetails(supportRecord)
            If Not prepareResult.StateResult Then
                Return New ActionResult(Of RIPSSupportRecord) With {
                    .StatusCode = eStatusResult.WARNING,
                    .StateResult = False,
                    .Message = prepareResult.Message
                }
            End If

            Dim detailsList As List(Of Object) = prepareResult.ObjectEmbbeded
            Dim admission = _admissionRepository.GetAdmissionByCode(supportRecord.AdmissionNumber)

            If admission Is Nothing Then
                Return New ActionResult(Of RIPSSupportRecord) With {
                    .StatusCode = eStatusResult.WARNING,
                    .StateResult = False,
                    .Message = "No se encontró el ingreso asociado al soporte RIPS"
                }
            End If

            ' Crear objeto args
            Dim args As Object = CreateServiceOrderArgs(admission, detailsList)

            Dim careGroup As CareGroup = _careGroupRepository.GetCareGroupById(args.CareGroupId)

            If careGroup Is Nothing OrElse careGroup.Id <= 0 Then
                Return New ActionResult(Of RIPSSupportRecord) With {
                    .StatusCode = eStatusResult.WARNING,
                    .StateResult = False,
                    .Message = "No se encontró el grupo de atención asociado al soporte RIPS"
                }
            End If

            ' Validar contrato si es EAPB
            Dim contractValidationResult = ValidateContractForCareGroup(careGroup)
            If Not contractValidationResult.StateResult Then
                Return New ActionResult(Of RIPSSupportRecord) With {
                    .StatusCode = eStatusResult.WARNING,
                    .StateResult = False,
                    .Message = contractValidationResult.Message
                }
            End If

            Dim messageNotificationContract As String = If(contractValidationResult.MessageResult IsNot Nothing AndAlso contractValidationResult.MessageResult.Count > 0,
                                                           String.Join(vbCrLf, contractValidationResult.MessageResult.ToArray()),
                                                           String.Empty)

            ' Transacción
            Using transactionScope As New TransactionScope(TransactionScopeOption.RequiresNew,
                                                           New TransactionOptions() With {
                                                               .Timeout = TransactionManager.MaximumTimeout,
                                                               .IsolationLevel = IsolationLevel.ReadCommitted
                                                           },
                                                           TransactionScopeAsyncFlowOption.Enabled)
                Try
                    ' Preparar ServiceOrder para actualización (eliminar detalles existentes y agregar nuevos)
                    Dim resultPrepareUpdate = PrepareServiceOrderForUpdate(supportRecord, careGroup, operatingUnitId, args, audit)

                    If Not resultPrepareUpdate.StateResult OrElse resultPrepareUpdate.ObjectEmbbeded Is Nothing Then
                        Return New ActionResult(Of RIPSSupportRecord) With {
                            .StatusCode = eStatusResult.WARNING,
                            .StateResult = False,
                            .Message = If(resultPrepareUpdate.Message, "No se pudo preparar la orden de servicio para actualización")
                        }
                    End If

                    ' Guardar ServiceOrder actualizada
                    Dim _idCurrentSequence As Integer = 0
                    Dim resultSaveServiceOrder = _serviceOrderAdminService.SaveServiceOrder(resultPrepareUpdate.ObjectEmbbeded, audit, _idCurrentSequence)

                    If resultSaveServiceOrder Is Nothing OrElse Not resultSaveServiceOrder.StateResult Then
                        Return New ActionResult(Of RIPSSupportRecord) With {
                            .StatusCode = eStatusResult.WARNING,
                            .StateResult = False,
                            .Message = If(resultSaveServiceOrder?.Message, "Error al actualizar la orden de servicio")
                        }
                    End If

                    Dim savedServiceOrder As ServiceOrder = resultSaveServiceOrder.ObjectEmbbeded

                    ' Asignar campos de auditoría para ACTUALIZACIÓN
                    supportRecord.ModificationUser = audit.CodeUser
                    supportRecord.ModificationDate = Date.Now
                    supportRecord.ServiceOrder = Nothing ' Evitar tracking duplicado

                    ' Guardar RIPSSupportRecord actualizado
                    _RIPSSupportRecordRepository.SaveEntity(supportRecord)
                    _RIPSSupportRecordRepository.UnitWork.Commit()

                    transactionScope.Complete()

                    Dim successMessage As String = String.Format("Registro de soporte RIPS actualizado exitosamente. Orden de servicio actualizada (Código: {0}). Registro RIPS (Código: {1}).",
                                                                 If(savedServiceOrder?.Code, "N/A"),
                                                                 If(supportRecord?.Code, "N/A"))

                    If Not String.IsNullOrEmpty(messageNotificationContract) Then
                        successMessage = String.Concat(successMessage, vbCrLf, messageNotificationContract)
                    End If

                    Return New ActionResult(Of RIPSSupportRecord) With {
                        .StatusCode = eStatusResult.SUCCESS,
                        .StateResult = True,
                        .ObjectEmbbeded = supportRecord,
                        .Message = successMessage
                    }

                Catch ex As Exception
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                    Return New ActionResult(Of RIPSSupportRecord) With {
                        .StatusCode = eStatusResult.WARNING,
                        .StateResult = False,
                        .Message = String.Format("Error al actualizar el soporte RIPS: {0}", IndigoManagementExceptions.GetExceptionDetails(ex))
                    }
                End Try
            End Using

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RIPSSupportRecord) With {
                .StatusCode = eStatusResult.EXCEPTION,
                .StateResult = False,
                .Message = String.Format("Error inesperado en el escenario de actualización: {0}", IndigoManagementExceptions.GetExceptionDetails(ex))
            }
        End Try
    End Function

    ''' <summary>
    ''' Procesa el escenario de anulación de un RIPSSupportRecord existente y su ServiceOrder asociada
    ''' </summary>
    Private Async Function ProcessAnnulScenarioAsync(
        ByVal supportRecord As RIPSSupportRecord,
        ByVal audit As AuditMessage) As Task(Of ActionResult(Of RIPSSupportRecord))

        Try
            ' Transacción
            Using transactionScope As New TransactionScope(TransactionScopeOption.RequiresNew,
                                                           New TransactionOptions() With {
                                                               .Timeout = TransactionManager.MaximumTimeout,
                                                               .IsolationLevel = IsolationLevel.ReadCommitted
                                                           },
                                                           TransactionScopeAsyncFlowOption.Enabled)
                Try
                    ' Preparar ServiceOrder para anulación
                    Dim resultPrepareAnnul = PrepareServiceOrderForAnnulment(supportRecord)

                    If Not resultPrepareAnnul.StateResult OrElse resultPrepareAnnul.ObjectEmbbeded Is Nothing Then
                        Return New ActionResult(Of RIPSSupportRecord) With {
                            .StatusCode = eStatusResult.WARNING,
                            .StateResult = False,
                            .Message = If(resultPrepareAnnul.Message, "No se pudo preparar la orden de servicio para anulación")
                        }
                    End If

                    ' Guardar ServiceOrder anulada (el SP se encarga del proceso completo de anulación)
                    Dim _idCurrentSequence As Integer = 0
                    Dim resultSaveServiceOrder = _serviceOrderAdminService.SaveServiceOrder(resultPrepareAnnul.ObjectEmbbeded, audit, _idCurrentSequence)

                    If resultSaveServiceOrder Is Nothing OrElse Not resultSaveServiceOrder.StateResult Then
                        Return New ActionResult(Of RIPSSupportRecord) With {
                            .StatusCode = eStatusResult.WARNING,
                            .StateResult = False,
                            .Message = If(resultSaveServiceOrder?.Message, "Error al anular la orden de servicio")
                        }
                    End If

                    Dim savedServiceOrder As ServiceOrder = resultSaveServiceOrder.ObjectEmbbeded

                    ' Asignar campos de auditoría para ANULACIÓN
                    supportRecord.Status = RIPSSupportRecordStatus.Annulled
                    supportRecord.ModificationUser = audit.CodeUser
                    supportRecord.ModificationDate = Date.Now
                    supportRecord.ServiceOrder = Nothing ' Evitar tracking duplicado

                    ' Guardar RIPSSupportRecord anulado
                    _RIPSSupportRecordRepository.SaveEntity(supportRecord)
                    _RIPSSupportRecordRepository.UnitWork.Commit()

                    transactionScope.Complete()

                    Dim successMessage As String = String.Format("Registro de soporte RIPS anulado exitosamente. Orden de servicio anulada (Código: {0}). Registro RIPS (Código: {1}).",
                                                                 If(savedServiceOrder?.Code, "N/A"),
                                                                 If(supportRecord?.Code, "N/A"))

                    Return New ActionResult(Of RIPSSupportRecord) With {
                        .StatusCode = eStatusResult.SUCCESS,
                        .StateResult = True,
                        .ObjectEmbbeded = supportRecord,
                        .Message = successMessage
                    }

                Catch ex As Exception
                    IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                    Return New ActionResult(Of RIPSSupportRecord) With {
                        .StatusCode = eStatusResult.WARNING,
                        .StateResult = False,
                        .Message = String.Format("Error al anular el soporte RIPS: {0}", IndigoManagementExceptions.GetExceptionDetails(ex))
                    }
                End Try
            End Using

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RIPSSupportRecord) With {
                .StatusCode = eStatusResult.EXCEPTION,
                .StateResult = False,
                .Message = String.Format("Error inesperado en el escenario de anulación: {0}", IndigoManagementExceptions.GetExceptionDetails(ex))
            }
        End Try
    End Function

#End Region

#Region "Métodos Auxiliares"

    ''' <summary>
    ''' Prepara la lista de detalles para la orden de servicio a partir de los RIPSSupportRecordDetail
    ''' </summary>
    Private Function PrepareServiceOrderDetails(ByVal supportRecord As RIPSSupportRecord) As ActionResult(Of List(Of Object))
        Try
            Dim supportDetails As List(Of RIPSSupportRecordDetail) = supportRecord.RIPSSupportRecordDetail.ToList()
            Dim detailsList As New ConcurrentBag(Of Object)

            Parallel.ForEach(Of RIPSSupportRecordDetail)(supportDetails,
                Sub(detail)
                    If detail Is Nothing Then Return

                    Dim daysStay As Integer = If(detail.CalculatedDaysStay.HasValue AndAlso detail.CalculatedDaysStay.Value > 0, detail.CalculatedDaysStay.Value, 1)

                    For dayIndex As Integer = 0 To daysStay - 1
                        Dim expando As Object = New ExpandoObject()

                        expando.CupsEntityCode = detail.CUPSCode
                        expando.FunctionalUnitCode = detail.FunctionalUnitCode
                        expando.ProfessionalCode = detail.ProfessionalCode
                        expando.FunctionalUnitId = detail.FunctionalUnitId
                        expando.Date = detail.InitialDate.AddDays(dayIndex)
                        expando.ProfessionalSpecialistCode = detail.SpecialtyCode
                        expando.AdmissionCode = detail.AdmissionNumber
                        expando.PatientCode = detail.PatientCode
                        expando.Quantity = 1
                        expando.NitMedico = detail.NitMedico
                        expando.CUPSId = detail.StayCUPSEntityId
                        expando.ContractDescriptionId = detail.ContractDescriptionId
                        expando.CUPSEntityContractDescriptionId = detail.StayCUPSEntityContractDescriptionId
                        expando.InitialDate = detail.InitialDate
                        expando.EndDate = detail.FinalDate

                        detailsList.Add(expando)
                    Next
                End Sub)

            If detailsList.Count = 0 Then
                Return New ActionResult(Of List(Of Object)) With {
                    .StatusCode = eStatusResult.WARNING,
                    .StateResult = False,
                    .Message = "No se pudieron procesar los detalles del registro de soporte RIPS"
                }
            End If

            Return New ActionResult(Of List(Of Object)) With {
                .StatusCode = eStatusResult.SUCCESS,
                .StateResult = True,
                .ObjectEmbbeded = detailsList.ToList()
            }

        Catch ex As Exception
            Return New ActionResult(Of List(Of Object)) With {
                .StatusCode = eStatusResult.EXCEPTION,
                .StateResult = False,
                .Message = String.Format("Error al preparar los detalles: {0}", ex.Message)
            }
        End Try
    End Function

    ''' <summary>
    ''' Crea el objeto de argumentos para la generación de la orden de servicio
    ''' </summary>
    Private Function CreateServiceOrderArgs(ByVal admission As Object, ByVal detailsList As List(Of Object)) As Object
        Dim args As Object = New ExpandoObject()
        args.CareGroupId = admission.GENCAREGROUP
        args.AdmissionNumber = admission.NUMINGRES
        args.PatientCode = admission.IPCODPACI
        args.CareCenterCode = admission.CODCENATE
        args.HealthAdministratorId = 0
        args.ThirdPartyId = 0
        args.AuthorizationNumber = admission.IAUTORIZA
        args.IsProcedureQx = False
        args.IsAccountControlAmbulatory = False
        args.IsCurrentAdmission = True
        args.Details = detailsList
        Return args
    End Function

    ''' <summary>
    ''' Valida el contrato para grupos de atención tipo EAPB
    ''' </summary>
    Private Function ValidateContractForCareGroup(ByVal careGroup As CareGroup) As ActionResult
        Dim messageNotification As New List(Of String)()

        If careGroup.CareGroupType <> 1 Then
            Return New ActionResult With {.StateResult = True, .MessageResult = messageNotification}
        End If

        ' Validar que exista contrato para el grupo de atención tipo EAPB
        If careGroup.Contract Is Nothing Then
            Return New ActionResult With {
                .StateResult = False,
                .Message = "El grupo de atención tipo EAPB no tiene un contrato asociado"
            }
        End If

        If careGroup.Contract.Status = 2 OrElse careGroup.Contract.Status = 3 Then
            Dim messageContract = If(careGroup.Contract.Status = 3, "Terminado", "Suspendido")
            Return New ActionResult With {
                .StateResult = False,
                .Message = String.Format("No se puede generar la orden de servicio debido a que el contrato está {0}", messageContract)
            }
        End If

        ' Validaciones adicionales del contrato
        Dim resultContractValidation As ActionResult = ValidationsContract(careGroup.Contract)
        If Not resultContractValidation.StateResult Then
            Return New ActionResult With {
                .StateResult = False,
                .Message = resultContractValidation.Message
            }
        End If

        If resultContractValidation.MessageResult IsNot Nothing AndAlso resultContractValidation.MessageResult.Count > 0 Then
            messageNotification.AddRange(resultContractValidation.MessageResult)
        End If

        Return New ActionResult With {.StateResult = True, .MessageResult = messageNotification}
    End Function

#End Region

    ''' <summary>
    ''' Genera una orden de servicio a partir del grupo de atención y los detalles proporcionados
    ''' </summary>
    ''' <param name="careGroup">Grupo de atención asociado</param>
    ''' <param name="operatingUnitId">Identificador de la unidad operativa</param>
    ''' <param name="args">Argumentos dinámicos con los detalles del ingreso y servicios</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <param name="entityId">Id de la entidad asociada (RIPSSupportRecord)</param>
    ''' <param name="entityCode">Código de la entidad asociada (RIPSSupportRecord)</param>
    ''' <returns>ActionResult con la orden de servicio generada</returns>
    Public Function GenerateServiceOrder(ByVal careGroup As CareGroup, ByVal operatingUnitId As Integer, ByVal args As Object, ByVal audit As AuditMessage, ByVal entityId As Integer, ByVal entityCode As String) As ActionResult(Of ServiceOrder)
        Try
            ' Validación de parámetros de entrada
            Dim validationResult = ValidateGenerateServiceOrderInput(careGroup, operatingUnitId, args)
            If validationResult IsNot Nothing Then
                Return validationResult
            End If

            ' Obtener paciente y su tercero de administradora de salud
            Dim patient As INPACIENT = _patientRepository.GetOnlyPatientByIdentification(args.PatientCode.ToString().Trim(), False)
            If patient Is Nothing Then
                Return New ActionResult(Of ServiceOrder) With {
                    .StatusCode = eStatusResult.WARNING,
                    .StateResult = False,
                    .Message = String.Format("No se encontró el paciente con código {0}", args.PatientCode)
                }
            End If

            Dim ThirdPartyPatientId As Integer = GetThirdPartyPatientId(patient)

            ' Obtener homologaciones de CUPS
            Dim resultHomologation As ActionResult(Of List(Of List(Of CupsHomologation))) = GetHomologationsCups(CType(args.Details, List(Of Object)), careGroup.Id, False)
            If Not resultHomologation.StateResult Then
                Return New ActionResult(Of ServiceOrder) With {
                    .StatusCode = eStatusResult.WARNING,
                    .StateResult = False,
                    .Message = resultHomologation.Message
                }
            End If

            Dim homologations As List(Of List(Of CupsHomologation)) = resultHomologation.ObjectEmbbeded

            ' Crear la orden de servicio
            Dim serviceOrder As New ServiceOrder()
            With serviceOrder
                .Code = String.Empty
                .AdmissionNumber = args.AdmissionNumber.ToString().Trim()
                .PatientCode = args.PatientCode.ToString().Trim()
                .OrderDate = Date.Now
                .OperatingUnitId = operatingUnitId
                .EntityName = "RIPSSupportRecord"
                .EntityId = entityId
                .EntityCode = If(String.IsNullOrWhiteSpace(entityCode), String.Empty, entityCode)
                .Status = 1
            End With

            Dim careCenterCode As String = CType(args.CareCenterCode, String).Trim()
            Dim Identity As Integer = 0

            ' Procesar cada detalle y crear los detalles de la orden de servicio
            For Each detail As Object In CType(args.Details, List(Of Object)).ToList()
                Identity += 1

                Dim resultDetail = ProcessServiceOrderDetail(
                    detail,
                    serviceOrder,
                    careGroup,
                    homologations,
                    patient,
                    careCenterCode,
                    ThirdPartyPatientId,
                    Identity,
                    args.AuthorizationNumber.ToString())

                If Not resultDetail.StateResult Then
                    Return New ActionResult(Of ServiceOrder) With {
                        .StatusCode = eStatusResult.WARNING,
                        .StateResult = False,
                        .Message = resultDetail.Message
                    }
                End If

                serviceOrder.ServiceOrderDetail.Add(resultDetail.ObjectEmbbeded)
            Next

            Return New ActionResult(Of ServiceOrder) With {
                .StatusCode = eStatusResult.SUCCESS,
                .StateResult = True,
                .ObjectEmbbeded = serviceOrder
            }
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ServiceOrder) With {
                .StatusCode = eStatusResult.WARNING,
                .StateResult = False,
                .Message = IndigoManagementExceptions.GetExceptionDetails(ex)
            }
        End Try
    End Function

    ''' <summary>
    ''' Valida los parámetros de entrada para la generación de orden de servicio
    ''' </summary>
    Private Function ValidateGenerateServiceOrderInput(ByVal careGroup As CareGroup, ByVal operatingUnitId As Integer, ByVal args As Object) As ActionResult(Of ServiceOrder)
        If careGroup Is Nothing OrElse careGroup.Id <= 0 Then
            Return New ActionResult(Of ServiceOrder) With {
                .StatusCode = eStatusResult.WARNING,
                .StateResult = False,
                .Message = "El grupo de atención es requerido"
            }
        End If

        If operatingUnitId < 0 Then
            Return New ActionResult(Of ServiceOrder) With {
                .StatusCode = eStatusResult.WARNING,
                .StateResult = False,
                .Message = "El identificador de la unidad operativa no es válido"
            }
        End If

        If args Is Nothing Then
            Return New ActionResult(Of ServiceOrder) With {
                .StatusCode = eStatusResult.WARNING,
                .StateResult = False,
                .Message = "Los argumentos de la orden de servicio son requeridos"
            }
        End If

        Dim argsDict = TryCast(args, IDictionary(Of String, Object))
        If argsDict Is Nothing OrElse Not argsDict.ContainsKey("Details") OrElse argsDict("Details") Is Nothing Then
            Return New ActionResult(Of ServiceOrder) With {
                .StatusCode = eStatusResult.WARNING,
                .StateResult = False,
                .Message = "Los detalles de la orden de servicio son requeridos"
            }
        End If

        Dim details = TryCast(argsDict("Details"), List(Of Object))
        If details Is Nothing OrElse details.Count = 0 Then
            Return New ActionResult(Of ServiceOrder) With {
                .StatusCode = eStatusResult.WARNING,
                .StateResult = False,
                .Message = "Debe existir al menos un detalle para generar la orden de servicio"
            }
        End If

        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene el Id del tercero de la administradora de salud del paciente
    ''' </summary>
    Private Function GetThirdPartyPatientId(ByVal patient As INPACIENT) As Integer
        If patient Is Nothing OrElse patient.GENCONENTITY Is Nothing OrElse patient.GENCONENTITY <= 0 Then
            Return 0
        End If

        Dim healthAdministrator As HealthAdministrator = _healthAdministratorRepository.GetHealthAdministratorById(patient.GENCONENTITY)
        If healthAdministrator IsNot Nothing AndAlso healthAdministrator.Id > 0 Then
            Return healthAdministrator.ThirdPartyId
        End If

        Return 0
    End Function

    ''' <summary>
    ''' Procesa un detalle individual y crea el ServiceOrderDetail correspondiente
    ''' </summary>
    Private Function ProcessServiceOrderDetail(
        ByVal detail As Object,
        ByVal serviceOrder As ServiceOrder,
        ByVal careGroup As CareGroup,
        ByVal homologations As List(Of List(Of CupsHomologation)),
        ByVal patient As INPACIENT,
        ByVal careCenterCode As String,
        ByVal ThirdPartyPatientId As Integer,
        ByVal Identity As Integer,
        ByVal authorizationNumber As String) As ActionResult(Of ServiceOrderDetail)

        ' Validar unidad funcional
        Dim functionalUnit As Entities.FunctionalUnit = _functionalUnitRepository.GetFunctionalUnit(detail.FunctionalUnitCode.ToString().Trim(), False)
        If functionalUnit Is Nothing OrElse functionalUnit.Id = 0 Then
            Return New ActionResult(Of ServiceOrderDetail) With {
                .StatusCode = eStatusResult.WARNING,
                .StateResult = False,
                .Message = "No se encontró la unidad funcional " & detail.FunctionalUnitCode
            }
        End If

        ' Validar CUPS
        Dim cupsEntity As CUPSEntity = _cupsEntityRepository.GetCupsEntity(detail.CupsEntityCode.ToString().Trim())
        If cupsEntity Is Nothing OrElse cupsEntity.Id = 0 Then
            Return New ActionResult(Of ServiceOrderDetail) With {
                .StatusCode = eStatusResult.WARNING,
                .StateResult = False,
                .Message = "No se encontró el CUPS " & detail.CupsEntityCode
            }
        End If

        ' Validar tercero del médico
        Dim medicoThirdParty = _thirdPartyRepository.GetThirdPartyByNit(detail.NitMedico.ToString().TrimStart("0"), False)
        If medicoThirdParty Is Nothing OrElse medicoThirdParty.Id = 0 Then
            Return New ActionResult(Of ServiceOrderDetail) With {
                .StatusCode = eStatusResult.WARNING,
                .StateResult = False,
                .Message = "No se encontró el tercero del médico " & detail.NitMedico
            }
        End If

        ' Validar homologación
        Dim homologation = homologations.Find(Function(h) h IsNot Nothing AndAlso h.Count > 0 AndAlso h(0).CupsEntityId = cupsEntity.Id)
        If homologation Is Nothing Then
            Return New ActionResult(Of ServiceOrderDetail) With {
                .StatusCode = eStatusResult.WARNING,
                .StateResult = False,
                .Message = "No se encontró la homologación para el CUPS " & detail.CupsEntityCode
            }
        End If

        ' Obtener valores del detalle
        Dim functionalUnitId As Integer = CType(detail.FunctionalUnitId, Integer)
        Dim professionalSpecialistCode As String = CType(detail.ProfessionalSpecialistCode, String).Trim()
        Dim serviceDate As Date = CType(detail.Date, Date)
        Dim professionalCode As String = CType(detail.ProfessionalCode, String).Trim()
        Dim contractDescriptionId As Integer = CType(detail.ContractDescriptionId, Integer)
        Dim quantity As Integer = CType(detail.Quantity, Integer)

        ' Obtener el detalle de la orden de servicio con valores calculados
        Dim resultServiceOrderDetail As ActionResult(Of List(Of ServiceOrderDetail)) = _serviceOrderDetailAdminService.GetValueServiceWithRefactorValue(
            serviceOrder.AdmissionNumber,
            careCenterCode,
            homologation,
            careGroup.Id,
            functionalUnitId,
            professionalSpecialistCode,
            serviceDate,
            patient.IPSEXOPAC,
            patient.IPFECNACI,
            quantity,
            professionalCode,
            medicoThirdParty.Id,
            0,
            contractDescriptionId)

        If Not resultServiceOrderDetail.StateResult Then
            Return New ActionResult(Of ServiceOrderDetail) With {
                .StatusCode = eStatusResult.WARNING,
                .StateResult = False,
                .Message = If(resultServiceOrderDetail.Message, "Error al calcular el valor del servicio")
            }
        End If

        If resultServiceOrderDetail.ObjectEmbbeded Is Nothing OrElse resultServiceOrderDetail.ObjectEmbbeded.Count = 0 Then
            Return New ActionResult(Of ServiceOrderDetail) With {
                .StatusCode = eStatusResult.WARNING,
                .StateResult = False,
                .Message = "No se pudo obtener el detalle de la orden de servicio para el CUPS " & detail.CupsEntityCode
            }
        End If

        Dim serviceOrderDetail As ServiceOrderDetail = resultServiceOrderDetail.ObjectEmbbeded(0)
        serviceOrderDetail.CodeNameCareGroup = String.Concat(careGroup.Code, " - ", careGroup.Name)

        ' Asignar tercero según tipo de grupo de atención
        Select Case careGroup.CareGroupType
            Case 1 ' EAPB Con contrato
                If careGroup.ContractId IsNot Nothing AndAlso careGroup.Contract IsNot Nothing Then
                    serviceOrderDetail.ThirdPartyId = careGroup.Contract.ThirdPartyId
                    serviceOrderDetail.HealthAdministratorId = careGroup.Contract.HealthAdministratorId
                End If
            Case 3 ' Particulares
                If ThirdPartyPatientId > 0 Then
                    serviceOrderDetail.ThirdPartyId = ThirdPartyPatientId
                End If
        End Select

        serviceOrderDetail.ApplyRIAS = Nothing
        serviceOrderDetail.RIASCupsId = Nothing
        serviceOrderDetail.CUPSEntityContractDescriptionId = Nothing

        ' Asignar descripción del contrato si existe
        Dim detailDict = TryCast(detail, IDictionary(Of String, Object))
        If detailDict IsNot Nothing AndAlso detailDict.ContainsKey("CUPSEntityContractDescriptionId") AndAlso
           detailDict("CUPSEntityContractDescriptionId") IsNot Nothing AndAlso CInt(detail.CUPSEntityContractDescriptionId) > 0 Then

            serviceOrderDetail.CUPSEntityContractDescriptionId = CInt(detail.CUPSEntityContractDescriptionId)
            Dim CupsContractDes = _cupsEntityContractDescriptionsRepository.GetByFilter(
                Function(x) x.Id = serviceOrderDetail.CUPSEntityContractDescriptionId, False, {"ContractDescriptions"}).FirstOrDefault

            If CupsContractDes IsNot Nothing AndAlso CupsContractDes.ContractDescriptions IsNot Nothing Then
                serviceOrderDetail.ContractDescriptionCodeName = $"{CupsContractDes.ContractDescriptions.Code} - {CupsContractDes.ContractDescriptions.Name}"
            End If
        End If

        serviceOrderDetail.IdTmp = If(serviceOrderDetail.IdTmp > 0, serviceOrderDetail.IdTmp, Identity)
        serviceOrderDetail.AuthorizationNumber = authorizationNumber
        serviceOrderDetail.CodeNameFunctionalUnit = String.Concat(functionalUnit.Code, " - ", functionalUnit.Name)

        Return New ActionResult(Of ServiceOrderDetail) With {
            .StatusCode = eStatusResult.SUCCESS,
            .StateResult = True,
            .ObjectEmbbeded = serviceOrderDetail
        }
    End Function

    ''' <summary>
    ''' Confirma un registro de soporte RIPS cambiando su estado a Confirmed (1).
    ''' Una vez confirmado, el registro no se puede modificar, solo anular y crear uno nuevo.
    ''' </summary>
    ''' <param name="id">Identificador del registro de soporte RIPS</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <returns>ActionResult con el registro actualizado o mensaje de error</returns>
    Public Async Function ConfirmRIPSSupportRecordAsync(ByVal id As Integer, ByVal audit As AuditMessage) As Task(Of ActionResult(Of RIPSSupportRecord)) Implements IRIPSSupportRecordAdminService.ConfirmRIPSSupportRecordAsync
        Try
            ' Validar parámetros de entrada
            If id <= 0 Then
                Return New ActionResult(Of RIPSSupportRecord) With {
                    .StatusCode = eStatusResult.WARNING,
                    .StateResult = False,
                    .Message = "El identificador del registro de soporte RIPS no es válido"
                }
            End If

            If audit Is Nothing OrElse String.IsNullOrWhiteSpace(audit.CodeUser) Then
                Return New ActionResult(Of RIPSSupportRecord) With {
                    .StatusCode = eStatusResult.WARNING,
                    .StateResult = False,
                    .Message = "La información de auditoría es requerida"
                }
            End If

            ' Obtener el registro existente
            Dim existingRecord As RIPSSupportRecord = Await _RIPSSupportRecordRepository.GetRIPSSupportRecordByIdAsync(id)

            If existingRecord Is Nothing Then
                Return New ActionResult(Of RIPSSupportRecord) With {
                    .StatusCode = eStatusResult.WARNING,
                    .StateResult = False,
                    .Message = String.Format("No se encontró el registro de soporte RIPS con Id {0}", id)
                }
            End If

            ' Validar que el registro no esté ya confirmado
            If existingRecord.Status = RIPSSupportRecordStatus.Confirmed Then
                Return New ActionResult(Of RIPSSupportRecord) With {
                    .StatusCode = eStatusResult.WARNING,
                    .StateResult = False,
                    .Message = String.Format("El registro de soporte RIPS '{0}' ya se encuentra confirmado. Para modificarlo debe anularse y crear uno nuevo.", existingRecord.Code)
                }
            End If

            ' Validar que el registro no esté anulado
            If existingRecord.Status = RIPSSupportRecordStatus.Annulled Then
                Return New ActionResult(Of RIPSSupportRecord) With {
                    .StatusCode = eStatusResult.WARNING,
                    .StateResult = False,
                    .Message = String.Format("El registro de soporte RIPS '{0}' se encuentra anulado y no puede ser confirmado.", existingRecord.Code)
                }
            End If

            ' Validar que tenga una orden de servicio asociada
            If Not existingRecord.ServiceOrderId.HasValue OrElse existingRecord.ServiceOrderId.Value <= 0 Then
                Return New ActionResult(Of RIPSSupportRecord) With {
                    .StatusCode = eStatusResult.WARNING,
                    .StateResult = False,
                    .Message = "El registro de soporte RIPS no tiene una orden de servicio asociada y no puede ser confirmado."
                }
            End If

            ' Cambiar el estado a Confirmed (1)
            existingRecord.Status = RIPSSupportRecordStatus.Confirmed
            existingRecord.ModificationUser = audit.CodeUser
            existingRecord.ModificationDate = Date.Now

            ' Guardar el registro actualizado
            _RIPSSupportRecordRepository.SaveEntity(existingRecord)
            _RIPSSupportRecordRepository.UnitWork.Commit()

            Return New ActionResult(Of RIPSSupportRecord) With {
                .StatusCode = eStatusResult.SUCCESS,
                .StateResult = True,
                .ObjectEmbbeded = existingRecord,
                .Message = String.Format("El registro de soporte RIPS '{0}' ha sido confirmado exitosamente. A partir de ahora no podrá ser modificado.", existingRecord.Code)
            }

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of RIPSSupportRecord) With {
                .StatusCode = eStatusResult.EXCEPTION,
                .StateResult = False,
                .Message = String.Format("Error al confirmar el registro de soporte RIPS: {0}", IndigoManagementExceptions.GetExceptionDetails(ex))
            }
        End Try
    End Function

    Public Async Function ListAllRIPSSupportRecordsAsync() As Task(Of List(Of RIPSSupportRecord)) Implements IRIPSSupportRecordAdminService.ListAllRIPSSupportRecordsAsync
        Try
            Return Await _RIPSSupportRecordRepository.ListAllRIPSSupportRecordsAsync()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Valida los parámetros de entrada para la creación de un nuevo registro de soporte RIPS
    ''' </summary>
    ''' <param name="supportRecord">Registro de soporte RIPS a validar</param>
    ''' <param name="operatingUnitId">Identificador de la unidad operativa</param>
    ''' <param name="audit">Información de auditoría</param>
    ''' <returns>ActionResult con el error si la validación falla, Nothing si es exitosa</returns>
    Private Function ValidateNewRIPSSupportRecordInput(ByVal supportRecord As RIPSSupportRecord, ByVal operatingUnitId As Integer, ByVal audit As AuditMessage) As ActionResult(Of RIPSSupportRecord)
        If supportRecord Is Nothing Then
            Return New ActionResult(Of RIPSSupportRecord) With {
                .StatusCode = eStatusResult.WARNING,
                .StateResult = False,
                .Message = "El registro de soporte RIPS no puede ser nulo"
            }
        End If

        If audit Is Nothing OrElse String.IsNullOrWhiteSpace(audit.CodeUser) Then
            Return New ActionResult(Of RIPSSupportRecord) With {
                .StatusCode = eStatusResult.WARNING,
                .StateResult = False,
                .Message = "La información de auditoría es requerida"
            }
        End If

        If operatingUnitId < 0 Then
            Return New ActionResult(Of RIPSSupportRecord) With {
                .StatusCode = eStatusResult.WARNING,
                .StateResult = False,
                .Message = "El identificador de la unidad operativa no es válido"
            }
        End If

        If String.IsNullOrWhiteSpace(supportRecord.AdmissionNumber) Then
            Return New ActionResult(Of RIPSSupportRecord) With {
                .StatusCode = eStatusResult.WARNING,
                .StateResult = False,
                .Message = "El número de ingreso es requerido"
            }
        End If

        If supportRecord.RIPSSupportRecordDetail Is Nothing OrElse Not supportRecord.RIPSSupportRecordDetail.Any() Then
            Return New ActionResult(Of RIPSSupportRecord) With {
                .StatusCode = eStatusResult.WARNING,
                .StateResult = False,
                .Message = "El registro de soporte RIPS debe contener al menos un detalle"
            }
        End If

        Return Nothing
    End Function

    Private Function ValidationsContract(contract As Domain.Entities.Contract)
        Dim messageNotification As New List(Of String)()
        Dim contractCodeName As String = String.Concat(contract.Code, " - ", contract.ContractName)
        If contract.TerminationControl = 3 Then
            'Terminación del contrato por valor del contrato
            If contract.NotificationValueType = 2 AndAlso contract.ExecuteValue > contract.ContractValue * contract.PercentageNotification / 100 Then
                '% del contrato o valor del contrato
                messageNotification.Add(String.Format("Atención: queda un saldo restante de {0} para la terminación del contrato {1}", (contract.ContractValue - contract.ExecuteValue).ToString("C0"), contractCodeName))
            ElseIf contract.NotificationValueType = 3 AndAlso contract.ExecuteValue > contract.NotificationValue Then
                'Valor del contrato
                messageNotification.Add(String.Format("Atención: queda un saldo restante de {0} para la terminación del contrato {1}", (contract.ContractValue - contract.ExecuteValue).ToString("C0"), contractCodeName))
            End If
        ElseIf contract.TerminationControl = 2 Then
            'Terminación del contrato por Fecha del contrato
            If contract.NotificationTimeType = 2 AndAlso Date.Now.AddDays(contract.NotificationDays) >= contract.EndDate Then
                'Dias de anterioridad
                messageNotification.Add(String.Format("Atención: La fecha del contrato vencerá en {0} días", contract.EndDate.Value.Day - Date.Now.AddDays(contract.NotificationDays).Day))
            End If
        ElseIf contract.TerminationControl = 4 Then
            'Terminación del contrato por Fecha o Valor del contrato
            If contract.NotificationValueType = 2 AndAlso contract.ExecuteValue > contract.ContractValue * contract.PercentageNotification / 100 Then
                '% del contrato o valor del contrato
                messageNotification.Add(String.Format("Atención: queda un saldo restante de {0} para la terminación del contrato {1}", (contract.ContractValue - contract.ExecuteValue).ToString("C0"), contractCodeName))
            ElseIf contract.NotificationValueType = 3 AndAlso contract.ExecuteValue > contract.NotificationValue Then
                'Valor del contrato
                messageNotification.Add(String.Format("Atención: queda un saldo restante de {0} para la terminación del contrato {1}", (contract.ContractValue - contract.ExecuteValue).ToString("C0"), contractCodeName))
            End If
            If contract.NotificationTimeType = 2 AndAlso Date.Now.AddDays(contract.NotificationDays) >= contract.EndDate Then
                'Dias de anterioridad
                messageNotification.Add(String.Format("Atención: La fecha del contrato vencerá en {0} días", contract.EndDate.Value.Day - Date.Now.AddDays(contract.NotificationDays).Day))
            End If
        End If
        Return New ActionResult With {.StateResult = True, .MessageResult = messageNotification}
    End Function

    Private Function GetHomologationsCups(details As List(Of Object), careGroupId As Integer, Optional IsProcedureQx As Boolean = False) As ActionResult(Of List(Of List(Of CupsHomologation)))
        'Validaciones de las homologaciones
        Dim errors As New StringBuilder()
        Dim cupsEntity As CUPSEntity = Nothing
        Dim functionalUnit As Domain.Payroll.Entities.FunctionalUnit = Nothing
        Dim homologationMultiple As Boolean = False
        Dim listHomologations As New List(Of List(Of CupsHomologation))()

        Dim itemsToAdd As New List(Of Object)


        For Each itemDetail In details
            functionalUnit = _functionalUnitRepository.GetFunctionalUnit(itemDetail.FunctionalUnitCode.ToString().Trim(), False)
            If functionalUnit Is Nothing OrElse functionalUnit.Id = 0 Then
                errors.AppendLine(String.Format("La unidad funcional {0} no se encuentra homologada dentro de Indigo Vie", itemDetail.FunctionalUnitCode.ToString().Trim()))
                Continue For
            End If

            Dim cupsEntityCode As String = itemDetail.CupsEntityCode.ToString().Trim()
            cupsEntity = _cupsEntityRepository.GetCupsEntity(cupsEntityCode)
            If cupsEntity Is Nothing OrElse cupsEntity.Id = 0 Then
                errors.AppendLine(String.Format("El cups {0} no se encuentra homologado dentro de Indigo Vie", cupsEntityCode))
                Continue For
            End If

            If itemDetail.NitMedico Is Nothing OrElse itemDetail.ProfessionalSpecialistCode = "999" OrElse itemDetail.ProfessionalCode = "999" Then
                If CType(itemDetail, IDictionary(Of String, Object)).ContainsKey("TipoSolicitud") AndAlso itemDetail.TipoSolicitud IsNot Nothing AndAlso itemDetail.TipoSolicitud <> 1 Then
                    Dim specialty = _specialityRepository.GetSpecialityByCode("999")
                    If specialty Is Nothing OrElse String.IsNullOrEmpty(specialty.CODESPECI) Then
                        Return New ActionResult(Of List(Of List(Of CupsHomologation))) With {.StateResult = False, .Message = "No existe especialidad con código 999 en Indigo Vie Cloud Platform", .MessageResult = New List(Of String)({"No existe especialidad con código 999 en Indigo Vie Cloud Platform"})}
                    End If
                    itemDetail.NitMedico = "999"
                    itemDetail.ProfessionalSpecialistCode = "999"
                End If
            End If


            Dim thirdPartyMedico As Domain.Entities.ThirdParty = _thirdPartyRepository.GetThirdPartyByNit(itemDetail.NitMedico.ToString().TrimStart("0"), False)
            If thirdPartyMedico Is Nothing OrElse thirdPartyMedico.Id = 0 Then
                errors.AppendLine(String.Format("El Médico con Nit {0} no está parametrizado en Indigo Vie", itemDetail.NitMedico.ToString().TrimStart("0")))
            End If

            Dim RiasId As Integer? = Nothing
            Dim ContractDescriptionId As Integer? = Nothing
            If CType(itemDetail, IDictionary(Of String, Object)).ContainsKey("RiasId") AndAlso CType(itemDetail, IDictionary(Of String, Object))("RiasId") IsNot Nothing AndAlso CInt(itemDetail.RiasId) > 0 Then
                RiasId = CInt(itemDetail.RiasId)
            End If
            If CType(itemDetail, IDictionary(Of String, Object)).ContainsKey("ContractDescriptionId") AndAlso CType(itemDetail, IDictionary(Of String, Object))("ContractDescriptionId") IsNot Nothing _
                AndAlso CInt(itemDetail.ContractDescriptionId) > 0 Then
                ContractDescriptionId = CInt(itemDetail.ContractDescriptionId)
            End If

            Dim homologation As ActionResult(Of List(Of CupsHomologation)) = _contractServices.GetHomologationCups(careGroupId, cupsEntity.Id, functionalUnit.Id, itemDetail.ProfessionalSpecialistCode.ToString().Trim(), CType(itemDetail.Date, Date), 0, 0, RiasId, ContractDescriptionId)
            If homologation.StateResult Then
                If homologation.ObjectEmbbeded.Count > 1 Then
                    homologationMultiple = True
                End If
                listHomologations.Add(homologation.ObjectEmbbeded)
            Else
                If (Not String.IsNullOrEmpty(homologation.Message)) AndAlso homologation.Message.Contains("No se encontro una definicion de tarifas en la fecha") AndAlso (itemDetail.CupsEntityCode IsNot Nothing) Then
                    errors.AppendLine(homologation.Message & " para el código " & itemDetail.CupsEntityCode)
                Else
                    errors.AppendLine(homologation.Message)
                End If
            End If
        Next
        If errors.Length > 0 Then
            Return New ActionResult(Of List(Of List(Of CupsHomologation))) With {.StateResult = False, .Message = errors.ToString()}
            'ElseIf homologationMultiple Then
            '    Return New ActionResult(Of List(Of List(Of CupsHomologation))) With {.StateResult = False, .ObjectEmbbeded = listHomologations, .MessageResult = New List(Of String)({"MULTIPLE"})}
        Else
            Return New ActionResult(Of List(Of List(Of CupsHomologation))) With {.StateResult = True, .ObjectEmbbeded = listHomologations}
        End If
    End Function

    Public Sub Dispose() Implements IDisposable.Dispose

    End Sub
End Class
