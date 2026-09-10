'***********************************************************************
' Assembly         : Application.Billing
' Author           : Juan F. Tamayo
' Created          : 2014-11-19
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Crystal
Imports Domain.Crystal.Service
Imports System.Data.Entity.Infrastructure
Imports Application.Billing
Imports System.Data.Entity.Core
Imports System.Transactions
Imports System.Dynamic
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Crystal.Entities

#End Region

Public Class StayAdminService
    Implements IStayAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de unidad funcional
    ''' </summary>
    Private _funtionalUnitRepository As IFunctionalUnitRepository

    ''' <summary>
    ''' Repositorio de CUPS
    ''' </summary>
    Private _cupsRepository As ICupsEntityRepository

    ''' <summary>
    ''' Repositorio de folios
    ''' </summary>
    Private _revenueControlDetailRepository As IRevenueControlDetailRepository

    ''' <summary>
    ''' Repositorio de estancias
    ''' </summary>
    Private _stayRepository As IStayRepository

    ''' <summary>
    ''' Servicio de dominio de estancias
    ''' </summary>
    Private _stayDomainService As IStayService

    ''' <summary>
    ''' Repositorio de secuencias numericas
    ''' </summary>
    Private _secuenceDRepository As IBillingSequenceDetailRepository

    ''' <summary>
    ''' Servicio de aplicación en de orden de servicio
    ''' </summary>
    Private _serviceOrderAdminService As IServiceOrderAdminService

    Private _ipsServiceRepository As IIPSServicesRepository
    Private _billingSequenceAdminService As IBillingSequenseAdminService
    Private _admissionRepository As IAdmissionRepository
    Private _revenueControlRepository As IRevenueControlRepository
    Private _thirdpartyRepository As IThirdPartyRepository
    Private _healthAdministratorRepository As IHealthAdministratorRepository

    ''' <summary>
    ''' Repositorio de parametros
    ''' </summary>
    Private _parameterRepository As IParameterRepository
#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="stayRepository">Repositorio de estancias</param>
    ''' <param name="stayDomainService">Servicio de dominio de estancias</param>
    ''' <param name="cupsRepository">Repositorio de CUPS</param>
    ''' <param name="funtionalUnitRepository">Repositorio de unidad funcional</param>
    ''' <param name="serviceOrderAdminService">Servicio de aplicación de orden de servicio</param>
    ''' <param name="secuenceDRepository">Repositorio de secuencias</param>
    ''' <param name="revenueControlDetailRepository">Repositorio de folios</param>
    Public Sub New(stayRepository As IStayRepository, stayDomainService As IStayService, cupsRepository As ICupsEntityRepository, funtionalUnitRepository As IFunctionalUnitRepository, serviceOrderAdminService As IServiceOrderAdminService,
                   secuenceDRepository As IBillingSequenceDetailRepository, revenueControlDetailRepository As IRevenueControlDetailRepository, ipsServiceRepository As IIPSServicesRepository,
                   billingSequenceAdminService As IBillingSequenseAdminService, revenueControlRepository As IRevenueControlRepository,
                   admissionRepository As IAdmissionRepository,
                   thirdpartyRepository As IThirdPartyRepository,
                   healthAdministratorRepository As IHealthAdministratorRepository, parameterRepository As IParameterRepository)
        Me._stayRepository = stayRepository
        Me._stayDomainService = stayDomainService
        Me._cupsRepository = cupsRepository
        _thirdpartyRepository = thirdpartyRepository
        _admissionRepository = admissionRepository
        _healthAdministratorRepository = healthAdministratorRepository
        Me._funtionalUnitRepository = funtionalUnitRepository
        Me._serviceOrderAdminService = serviceOrderAdminService
        Me._secuenceDRepository = secuenceDRepository
        Me._revenueControlDetailRepository = revenueControlDetailRepository
        Me._ipsServiceRepository = ipsServiceRepository
        _billingSequenceAdminService = billingSequenceAdminService
        _revenueControlRepository = revenueControlRepository
        Me._parameterRepository = parameterRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Lista las estancias por número de ingreso y estado
    ''' </summary>
    ''' <param name="admissionCode">Número de ingreso</param>
    ''' <param name="status">Estado de la estancia</param>
    ''' <param name="asNoTracking">Valor que indica si se consulta las estancias sin seguimiento</param>
    ''' <returns>La lista de estancias</returns>
    Public Function ListStaysByAdmissionCodeAndStatus(admissionCode As String, status As Domain.Crystal.Entities.StayStatusEnum, asNoTracking As Boolean) As ActionResult(Of List(Of Domain.Crystal.Entities.CHREGESTA)) Implements IStayAdminService.ListStaysByAdmissionCodeAndStatus
        Try
            Return New ActionResult(Of List(Of Domain.Crystal.Entities.CHREGESTA)) With {.StateResult = True, .ObjectEmbbeded = Me._stayRepository.ListStaysByAdmissionCodeAndStatus(admissionCode, status, asNoTracking)}
        Catch ex As OptimisticConcurrencyException
            Return New ActionResult(Of List(Of Domain.Crystal.Entities.CHREGESTA)) With {.StateResult = False, .Message = "{ERR0}", .MessageResult = New List(Of String)() From {ResourceManager.GetString("ErrorConcurrence")}}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of Domain.Crystal.Entities.CHREGESTA)) With {.StateResult = False, .Message = "{ERR0}", .MessageResult = New List(Of String)() From {ex.Message}}
        End Try
    End Function

    Public Function ListOfStaysByAdmission(admissionCode As String) As ActionResult(Of List(Of CHREGESTA)) Implements IStayAdminService.ListOfStaysByAdmission
        Try
            Dim data = _stayDomainService.ListOfStaysWithConfigurationByAdmission(admissionCode, eLiquidateStayOption.DefectoManualGrupoAtencion, Nothing, Nothing)
            Return New ActionResult(Of List(Of CHREGESTA)) With {.StateResult = True, .ObjectEmbbeded = data}
        Catch ex As IndigoValidationException
            Return New ActionResult(Of List(Of CHREGESTA)) With {.StateResult = False, .Message = ex.Message}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of CHREGESTA)) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    Public Function ListOfStaysByAdmissionToModel(admissionCode As String, audit As AuditMessage) As ActionResult(Of List(Of StayInfoModel)) Implements IStayAdminService.ListOfStaysByAdmissionToModel
        Try
            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {
                .Timeout = TransactionManager.MaximumTimeout,
                .IsolationLevel = IsolationLevel.ReadCommitted
            })
                Dim data = _stayDomainService.ListOfStaysWithConfigurationByAdmissionToModel(admissionCode, eLiquidateStayOption.DefectoManualGrupoAtencion, Nothing, audit, Nothing)
                scope.Complete()
                Return New ActionResult(Of List(Of StayInfoModel)) With {.StateResult = True, .ObjectEmbbeded = data}
            End Using
        Catch ex As IndigoValidationException
            Return New ActionResult(Of List(Of StayInfoModel)) With {.StateResult = False, .Message = ex.Message}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of StayInfoModel)) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Lista las estancias por número de ingreso que se encuentran liquidadas
    ''' </summary>
    ''' <param name="admissionCode">Número de ingreso</param>
    ''' <param name="asNoTracking">Valor opcional que indica si se consulta las estancias sin seguimiento</param>
    ''' <returns>La lista de estancias</returns>
    Public Function ListLiquidatedStaysByAdmissionCode(admissionCode As String, asNoTracking As Boolean) As ActionResult(Of List(Of Domain.Crystal.Entities.CHREGESTA)) Implements IStayAdminService.ListLiquidatedStaysByAdmissionCode
        Try
            Dim obj = Me._stayRepository.ListLiquidatedStaysByAdmissionCode(admissionCode, asNoTracking)
            Dim today As DateTime = DateTime.Now
            For Each est In obj
                Dim totalTime As TimeSpan = If(est.FECFINEST < est.FECINIEST, (today.Subtract(est.FECINIEST)), (est.FECFINEST.Subtract(est.FECINIEST)))
                est.TotalTime = String.Format(ResourceManager.GetString("TotalTime"), totalTime.Days, totalTime.Hours, totalTime.Minutes)
                est.TotalUnits = If(est.CHREGESTADET IsNot Nothing, est.CHREGESTADET.Sum(Function(d) d.CANTIDADLIQ), 0)
                For Each det In est.CHREGESTADET.Where(Function(d) d.CodeNameCups Is Nothing OrElse d.CodeNameCups.Equals(String.Empty)).ToList()
                    Dim resCups = Me._cupsRepository.GetCupsEntityById(det.GENCUPSLIQ)
                    det.CodeNameCups = (resCups.Code & "-" & resCups.Description)
                Next
                Dim listFolios = Me._revenueControlDetailRepository.ListStayFolios(est.ID, est.ADINGRESO.NUMINGRES.Trim())
                est.ListFolios = New List(Of Domain.Crystal.Entities.StayFolios)()
                For Each o In listFolios
                    est.ListFolios.Add(New Domain.Crystal.Entities.StayFolios With {.IdFolio = o.IdFolio, .NumberFolio = o.NumberFolio, .ValueFolio = o.ValueFolio})
                Next
            Next
            Return New ActionResult(Of List(Of Domain.Crystal.Entities.CHREGESTA)) With {.StateResult = (obj.Count > 0), .ObjectEmbbeded = obj}
        Catch ex As OptimisticConcurrencyException
            Return New ActionResult(Of List(Of Domain.Crystal.Entities.CHREGESTA)) With {.StateResult = False, .Message = "{ERR0}", .MessageResult = New List(Of String)() From {ResourceManager.GetString("ErrorConcurrence")}}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of Domain.Crystal.Entities.CHREGESTA)) With {.StateResult = False, .Message = "{ERR0}", .MessageResult = New List(Of String)() From {ex.Message}}
        End Try
    End Function

    ''' <summary>
    ''' Lista las estancias por número de ingreso que no se encuentran liquidadas
    ''' </summary>
    ''' <param name="admissionCode">Número de ingreso</param>
    ''' <param name="caregroupId">Id del grupo de atención</param>
    ''' <param name="medicalOrderDate">Fecha de la orden medica para hospitalización</param>
    ''' <param name="endDate">Fecha final de corte</param>
    ''' <param name="asNoTracking">Valor opcional que indica si se consulta las estancias sin seguimiento</param>
    ''' <returns>La lista de estancias</returns>
    Public Function ListDontLiquidatedStaysByAdmissionCode(admissionCode As String, caregroupId As Integer, stayOption As eLiquidateStayOption, medicalOrderDate As DateTime?, Optional endDate As DateTime? = Nothing, Optional asNoTracking As Boolean = True) As ActionResult(Of List(Of Domain.Crystal.Entities.CHREGESTA)) Implements IStayAdminService.ListDontLiquidatedStaysByAdmissionCode
        Try
            Dim obj = Me._stayDomainService.ListDontLiquidatedStays(admissionCode, caregroupId, stayOption, Nothing, endDate)
            If obj.StateResult Then
                Dim today As DateTime = DateTime.Now
                For Each est In obj.ObjectEmbbeded
                    Dim totalTime As TimeSpan = If(est.FECFINEST < est.FECINIEST, (today.Subtract(est.FECINIEST)), (est.FECFINEST.Subtract(est.FECINIEST)))
                    est.TotalTime = String.Format(ResourceManager.GetString("TotalTime"), totalTime.Days, totalTime.Hours, totalTime.Minutes)
                Next
            End If
            Return New ActionResult(Of List(Of Domain.Crystal.Entities.CHREGESTA)) With {.StateResult = obj.StateResult, .ObjectEmbbeded = obj.ObjectEmbbeded, .Message = obj.Message, .MessageResult = obj.MessageResult}
        Catch ex As OptimisticConcurrencyException
            Return New ActionResult(Of List(Of Domain.Crystal.Entities.CHREGESTA)) With {.StateResult = False, .Message = "{ERR0}", .MessageResult = New List(Of String)() From {ResourceManager.GetString("ErrorConcurrence")}}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of Domain.Crystal.Entities.CHREGESTA)) With {.StateResult = False, .Message = ex.Message, .MessageResult = New List(Of String)() From {ex.Message}}
        End Try
    End Function

    ''' <summary>
    ''' Calcula las unidades de estancia (días/horas) para un rango específico,
    ''' reutilizando Utils.CalcUnitStay y los parámetros del centro de atención.
    ''' </summary>
    Private Function ILiquidationAdminService_CalculateUnitStayForRange(admissionCode As String, startDate As Date, endDate As Date) As UnitStay Implements IStayAdminService.CalculateUnitStayForRange
        ' Cargar ingreso para obtener centro de atención
        Dim admission = _admissionRepository.FirstOrDefault(Function(m) m.NUMINGRES = admissionCode)

        If admission Is Nothing Then
            Throw New IndigoValidationException($"No se encontró el ingreso {admissionCode} para calcular la estancia.")
        End If

        ' Obtener parámetros del centro (GENHORACORTE, etc.)
        Dim parameters = _parameterRepository.FirstOrDefault(Function(m) m.CODCENATE = admission.CODCENATE)

        If parameters Is Nothing Then
            Throw New IndigoValidationException($"No existen parámetros para el centro de atención ({admission.CODCENATE}).")
        End If

        ' Usar la misma lógica de cálculo de unidades que el módulo de estancias
        Dim units As UnitStay = Utils.CalcUnitStay(startDate, endDate, parameters.GENHORACORTE)

        Return units
    End Function

    ''' <summary>
    ''' Gets the portfolio sequence by tag form.
    ''' </summary>
    ''' <param name="tag">The tag.</param>
    ''' <param name="idOperativeUnitId">The identifier operative unit identifier.</param>
    ''' <returns></returns>
    Private Function GetBillingSequenceByTagForm(tag As String, Optional idOperativeUnitId As Integer = 0) As ActionResult(Of String)
        Try
            'Consultar el id de la secuencia
            Dim _idCurrentSequence As Integer = 0
            Dim _sequence As BillingSequence = _billingSequenceAdminService.GetSequenseByIdForm(tag)
            If _sequence IsNot Nothing AndAlso _sequence.Id > 0 Then
                If _sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                    _idCurrentSequence = _sequence.BillingSequenceDetail(0).Id
                ElseIf _sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                    Dim res = (From ou As BillingSequenceDetail In _sequence.BillingSequenceDetail Where ou.OperatingUnit.Id = idOperativeUnitId Select ou).ToList()
                    If res IsNot Nothing AndAlso res.Count > 0 Then
                        _idCurrentSequence = res(0).Id
                    End If
                End If
                Return New ActionResult(Of String) With {.StateResult = True, .ObjectEmbbeded = _idCurrentSequence}
            Else
                Return New ActionResult(Of String) With {.StateResult = False, .Message = String.Format("No se encontró secuencia numérica para el Tag {0} de Facturación", tag)}
            End If
        Catch ex As Exception
            Return New ActionResult(Of String) With {.StateResult = False, .Message = String.Format("No se encontró secuencia numérica para el Tag {0} de Facturación", tag)}
        End Try
    End Function

    Public Function LiquidateStays(admissionCode As String, stays As List(Of StayInfoModel), operativeUnitId As Integer, audit As AuditMessage)
        Dim unitWork As IUnitWork = Me._stayRepository.UnitWork
        Try
            Dim admission = _admissionRepository.FirstOrDefault(Function(m) m.NUMINGRES = admissionCode, False)

            Using scope As New TransactionScope(
                TransactionScopeOption.Required,
                New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                Dim serviceOrder As New ServiceOrder With {
                    .Code = "",
                    .AdmissionNumber = admissionCode,
                    .PatientCode = admission.IPCODPACI,
                    .OrderDate = Date.Now,
                    .EntityCode = Nothing,
                    .EntityId = Nothing,
                    .EntityName = "AccountControl",
                    .OperatingUnitId = operativeUnitId,
                    .Status = 1,'Registrado              
                    .CreationDate = Date.Now,
                    .CreationUser = audit.CodeUser
                }


                For Each stay In stays 'Solo fitro los que tengan det
                    Dim est = stay.Stay
                    'Dim sod = GenerateServiceOrderDetailByStay(est, endDate, caregroupId, healthAdministratorId, thirdPartyId)
                    est.ADINGRESO.GENULTLIQUI = stay.EndDate

                    Dim estUnluidate = est.CHREGESTADET.Where(Function(o) o.ChangeTracker.State = ObjectState.Added).FirstOrDefault()
                    Me._stayRepository.SaveEntity(est)
                    Me._stayRepository.UnitWork.CommitAndRefreshChanges()

                    Dim sod As New ServiceOrderDetail()

                    If est.CupsId <> 0 Then
                        Dim cupsEntity = _cupsRepository.FirstOrDefault(Function(m) m.Id = est.CupsId, includes:={"BillingConcept"})
                        Dim ips As IPSService = _ipsServiceRepository.FirstOrDefault(Function(m) m.Id = est.IPSServiceId, False)
                        Dim func = _funtionalUnitRepository.FirstOrDefault(Function(m) m.Code = est.CHCAMASHO.INUNIFUNC.UFUCODIGO.Trim(), False)

                        If func.Id = 0 Then
                            Throw New IndigoValidationException(String.Format(ResourceManager.GetString("MessageDontFuntionalUnitHomologated"), est.CHCAMASHO.INUNIFUNC.UFUCODIGO))
                        End If

                        sod.CareGroupId = admission.GENCAREGROUP
                        sod.HealthAdministratorId = admission.GENCONENTITY
                        Dim health = _healthAdministratorRepository.FirstOrDefault(Function(m) m.Id = sod.HealthAdministratorId, False)
                        sod.ThirdPartyId = health?.ThirdPartyId

                        sod.RateManualId = est.RateManualId
                        sod.RateManualType = est.RateManualType
                        sod.RateManualDetailId = est.RateManualDetailId

                        sod.RecordType = 1
                        sod.CUPSEntityId = est.CupsId
                        sod.IPSServiceId = est.IPSServiceId
                        sod.CUPSEntityContractDescriptionId = est.CUPSEntityContractDescriptionId
                        sod.HospitalStayId = est.ID
                        sod.HospitalStayDetailId = estUnluidate.ID
                        sod.CUPSAssociateService = False
                        sod.LiquidationType = 5
                        sod.InvoicedQuantity = est.TotalUnits
                        sod.RateManualSalePrice = est.Value
                        sod.ServiceDate = est.FECINIEST
                        sod.Presentation = ips.Presentation
                        sod.PerformsFunctionalUnitId = func.Id
                        sod.BillingConceptId = cupsEntity.BillingConcept.Id
                        sod.CostCenterId = If(cupsEntity.BillingConcept.ObtainCostCenter = 1, func.CostCenterId, cupsEntity.BillingConcept.CostCenterId)
                        sod.SettlementType = 1
                        sod.SubTotalSalesPrice = est.Value
                        sod.TotalSalesPrice = est.Value
                        sod.GrandTotalSalesPrice = (est.Value * est.TotalUnits)
                        sod.SurchargeApply = False
                        sod.SurgeryNumber = 0
                        sod.IsFirstEvent = False
                    End If

                    serviceOrder.ServiceOrderDetail.Add(sod)
                Next

                Dim res = Me._serviceOrderAdminService.SaveServiceOrder(serviceOrder, audit, 0)
                If res.StateResult Then
                    unitWork.Commit()
                    scope.Complete()
                Else
                    unitWork.RollbackChangesUnitOfWork()
                    scope.Dispose()
                    Throw New IndigoValidationException(res.Message)
                End If

#If DEBUG Then
                scope.Dispose()
#Else
                scope.Complete()
#End If
                Return New ActionResult With {.StateResult = True}
            End Using
        Catch ex As IndigoValidationException
            Return New ActionResult With {.StateResult = False, .Message = ex.Message}
        Catch ex As Exception
            unitWork.RollbackChangesUnitOfWork()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult() With {.StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex), .MessageResult = New List(Of String)() From {ex.Message}}
        End Try
    End Function

    ''' <summary>
    ''' Realiza la liquidación de estancias según los parámetros, la persiste y crea la orden de servicio
    ''' </summary>
    ''' <param name="admissionCode">Número de ingreso</param>
    ''' <param name="caregroupId">Id del grupo de atención</param>
    ''' <param name="medicalOrderDate">Fecha de la orden medica para hospitalización</param>
    ''' <param name="endDate">Fecha final de corte</param>
    ''' <param name="patientCode">Código del paciente</param>
    ''' <param name="idSequence">Id de la secuencia numerica</param>
    ''' <param name="operatingUnitId">Id de la unidad operativa</param>
    ''' <param name="audit">Mensaje de auditoria</param>
    ''' <returns>Resultado de la acción</returns>
    Public Function LiquidateStays(
                    admissionCode As String,
                    healthAdministratorId As Integer,
                    thirdPartyId As Integer,
                    caregroupId As Integer,
                    stayOption As eLiquidateStayOption,
                    medicalOrderDate As Date?,
                    endDate As Date?,
                    patientCode As String,
                    idSequence As Integer,
                    operatingUnitId As Integer,
                    audit As AuditMessage) As ActionResult Implements IStayAdminService.LiquidateStays
        Dim unitWork As IUnitWork = Me._stayRepository.UnitWork
        If operatingUnitId = 0 Then
            Return New ActionResult With {.StateResult = False, .Message = "Seleccione una unidad operativa"}
        End If
        Dim _sequenceResult = GetBillingSequenceByTagForm("755", operatingUnitId)
        If Not _sequenceResult.StateResult Then
            Return New ActionResult With {.StateResult = False, .Message = _sequenceResult.Message}
        End If

        Try

            idSequence = _sequenceResult.ObjectEmbbeded
            Dim result As New ActionResult()
            result.Message = String.Empty
            result.MessageResult = New List(Of String)()
            result.StateResult = False
            Using transaction As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})
                Dim obj = Me._stayDomainService.ListDontLiquidatedStays(admissionCode, caregroupId, stayOption, medicalOrderDate, endDate, False)

                If obj.StateResult Then
                    'Se persiste y se genera la orden de servicio
                    Dim serv As New ServiceOrder()
                    serv.Code = ""
                    serv.AdmissionNumber = admissionCode
                    serv.PatientCode = patientCode
                    serv.OrderDate = Date.Now
                    serv.EntityCode = Nothing
                    serv.EntityId = Nothing
                    serv.EntityName = "LiquidateStays"
                    serv.OperatingUnitId = operatingUnitId
                    serv.Status = 1 'Registrado
                    serv.CreationDate = Date.Now
                    serv.CreationUser = audit.CodeUser
                    If endDate Is Nothing Then
                        endDate = obj.ObjectEmbbeded.Max(Function(o) o.FECFINEST)
                    End If

                    For Each est In obj.ObjectEmbbeded.Where(Function(x) x.CHREGESTADET.Any(Function(y) y.ChangeTracker.State = ObjectState.Added)) 'Solo fitro los que tengan det
                        Dim sod = GenerateServiceOrderDetailByStay(est, endDate, caregroupId, healthAdministratorId, thirdPartyId)
                        serv.ServiceOrderDetail.Add(sod)
                    Next

                    Dim res1 = Me._serviceOrderAdminService.SaveServiceOrder(serv, audit, idSequence)
                    If res1.StateResult Then
                        unitWork.Commit()
                        transaction.Complete()
                        result.StateResult = True
                    Else
                        unitWork.RollbackChangesUnitOfWork()
                        transaction.Dispose()
                        result.Message = res1.Message
                        result.MessageResult.Add(res1.Message)
                    End If
                Else
                    result.StateResult = False
                    result.MessageResult = obj.MessageResult
                    result.Message = obj.Message
                End If
            End Using

            Return result
        Catch ex As OptimisticConcurrencyException
            unitWork.RollbackChangesUnitOfWork()
            Return New ActionResult() With {.StateResult = False, .Message = "{ERR0}", .MessageResult = New List(Of String)() From {ResourceManager.GetString("ErrorConcurrence")}}
        Catch ex As Exception
            unitWork.RollbackChangesUnitOfWork()
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult() With {.StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex), .MessageResult = New List(Of String)() From {ex.Message}}
        End Try
    End Function

    Private Function GenerateServiceOrderDetailByStay(est As CHREGESTA, endDate As Date, careGroupId As Integer, healthAdministratorId As Integer, thirdPartyId As Integer) As ServiceOrderDetail
        est.ADINGRESO.GENULTLIQUI = endDate

        Dim estUnluidate = est.CHREGESTADET.Where(Function(o) o.ChangeTracker.State = ObjectState.Added).FirstOrDefault()
        Me._stayRepository.SaveEntity(est)
        Me._stayRepository.UnitWork.CommitAndRefreshChanges()

        Dim det As New ServiceOrderDetail()

        If est.CupsId <> 0 Then
            Dim cupsEntity = _cupsRepository.FirstOrDefault(Function(m) m.Id = est.CupsId, includes:={"BillingConcept"})
            Dim ips As IPSService = _ipsServiceRepository.FirstOrDefault(Function(m) m.Id = est.IPSServiceId, False)
            Dim func = _funtionalUnitRepository.FirstOrDefault(Function(m) m.Code = est.CHCAMASHO.INUNIFUNC.UFUCODIGO.Trim(), False)

            If func.Id = 0 Then
                Throw New IndigoValidationException(String.Format(ResourceManager.GetString("MessageDontFuntionalUnitHomologated"), est.CHCAMASHO.INUNIFUNC.UFUCODIGO))
            End If

            det.CareGroupId = careGroupId

            If healthAdministratorId > 0 Then det.HealthAdministratorId = healthAdministratorId

            If thirdPartyId > 0 Then det.ThirdPartyId = thirdPartyId

            det.RateManualId = est.RateManualId
            det.RateManualType = est.RateManualType
            det.RateManualDetailId = est.RateManualDetailId

            det.RecordType = 1
            det.CUPSEntityId = est.CupsId
            det.IPSServiceId = est.IPSServiceId
            det.CUPSEntityContractDescriptionId = est.CUPSEntityContractDescriptionId
            det.HospitalStayId = est.ID
            det.HospitalStayDetailId = estUnluidate.ID
            det.CUPSAssociateService = False
            det.LiquidationType = 5
            det.InvoicedQuantity = est.TotalUnits
            det.RateManualSalePrice = est.Value
            det.ServiceDate = est.FECINIEST
            det.Presentation = ips.Presentation
            det.PerformsFunctionalUnitId = func.Id
            det.BillingConceptId = cupsEntity.BillingConcept.Id
            det.CostCenterId = If(cupsEntity.BillingConcept.ObtainCostCenter = 1, func.CostCenterId, cupsEntity.BillingConcept.CostCenterId)
            det.SettlementType = 1
            det.SubTotalSalesPrice = est.Value
            det.TotalSalesPrice = est.Value
            det.GrandTotalSalesPrice = (est.Value * est.TotalUnits)
            det.SurchargeApply = False
            det.SurgeryNumber = 0
            det.IsFirstEvent = False
        End If

        Return det
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _stayDomainService.Dispose()
                _serviceOrderAdminService.Dispose()
                _billingSequenceAdminService.Dispose()
            End If
            _stayRepository = Nothing
            _stayDomainService = Nothing
            _cupsRepository = Nothing
            _funtionalUnitRepository = Nothing
            _serviceOrderAdminService = Nothing
            _secuenceDRepository = Nothing
            _revenueControlDetailRepository = Nothing
            _ipsServiceRepository = Nothing
            _billingSequenceAdminService = Nothing
            _revenueControlRepository = Nothing
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