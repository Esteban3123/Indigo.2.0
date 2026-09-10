'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 30-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Text
Imports System.Threading.Tasks
Imports System.Transactions
Imports Domain.Base.Entities
Imports Domain.Crystal
Imports Domain.Crystal.Entities
Imports Domain.Crystal.Service
Imports Domain.Entities
Imports Domain.Entities.Service
Imports Domain.Payroll
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources
Imports Newtonsoft.Json

Public Class AccountControlAdminService
    Implements IAccountControlAdminService

#Region "Fields"
    'ChangeRateServices

    Private _contractServices As IContractServices
    Private _billingServices As IBillingServices
    Private _careGroupRepository As ICareGroupRepository
    Private _cupsEntityRepository As ICupsEntityRepository
    Private _functionalUnitRepository As IFunctionalUnitRepository
    Private _serviceOrderDetailAdminService As IServiceOrderDetailAdminService
    Private _patientRepository As IPatientRepository
    Private _thidPartyRepository As IThirdPartyRepository
    Private _serviceOrderAdminService As IServiceOrderAdminService
    Private _billingSequenceAdminService As IBillingSequenseAdminService
    Private _hCORDLABORepository As IHCORDLABORepository
    Private _costCenterRepository As ICostCenterRepository
    Private _healthProfessionalRepository As IHealthProfessionalRepository
    Private _hCORDPROQRepository As IHCORDPROQRepository
    Private _hCQXINFORRepository As IHCQXINFORRepository
    Private _hCORDIMAGRepository As IHCORDIMAGRepository
    Private _hCORDPRONRepository As IHCORDPRONRepository
    Private _hCPLAOTRPROCUPSRepository As IHCPLAOTRPROCUPSRepository
    Private _hCORDPATORepository As IHCORDPATORepository
    Private _hCPROCTERRepository As IHCPROCTERRepository
    Private _hCORDINTERepository As IHCORDINTERepository
    Private _hCHOGASINRepository As IHCHOGASINRepository
    Private _hcCONOXIGRepository As IHCCONOXIGRepository
    Private _hcHISPACARepository As IHCHISPACARepository
    Private _hcORHEMSERRepository As IHCORHEMSERRepository
    'Private _AGCITASERIPSRepository As IAGCITASERIPSRepository
    Private _rateManualRepository As IRateManualRepository
    Private _cupsHomologation As ICupsHomologationRepository
    Private _healthAdministratorRepository As IHealthAdministratorRepository
    Private _rateManualDetailRepository As IRateManualDetailRepository
    Private _ipsServiceRepository As IIPSServicesRepository
    Private _billingConceptRepository As IBillingConceptRepository
    Private _hCQXREALIRepository As IHCQXREALIRepository
    Private _hCQXEQUIPRepository As IHCQXEQUIPRepository
    Private _specialityRepository As ISpecialityRepository
    Private _accountControlAdminService As IAccountControlAdminService
    Private _billingControlRepository As IBillingControlRepository
    Private _settingInventoryRepository As ISettingInventoryRepository
    Private _productGroupsRepository As IProductGroupsRepository
    Private _warehouseRepository As IWarehouseRepository
    Private _pharmaDoseRepository As IPharmaDoseRepository
    Private _cupsEntityContractDescriptionsRepository As ICupsEntityContractDescriptionsRepository
    Private ReadOnly _stayRepository As IStayRepository
    Private ReadOnly _accountControlStayRepository As IAccountControlStayRepository
    Private ReadOnly _serviceOrderDetailRepository As IServiceOrderDetailRepository
    Private _aMBORDIMARepository As IAMBORDIMARepository
    Private _aMBORDLABRepository As IAMBORDLABRepository
#End Region

#Region "Methods"

    Public Sub New(ByVal contractServices As IContractServices, careGroupRepository As ICareGroupRepository, cupsEntityRepository As ICupsEntityRepository,
                   functionalUnitRepository As IFunctionalUnitRepository, billingServices As IBillingServices, serviceOrderDetailAdminService As IServiceOrderDetailAdminService,
                   patientRepository As IPatientRepository, thidPartyRepository As IThirdPartyRepository, serviceOrderAdminService As IServiceOrderAdminService,
                   billingSequenceAdminService As IBillingSequenseAdminService, hCORDLABORepository As IHCORDLABORepository, costCenterRepository As ICostCenterRepository,
                   healthProfessionalRepository As IHealthProfessionalRepository, hCORDPROQRepository As IHCORDPROQRepository, hCQXINFORRepository As IHCQXINFORRepository,
                   hCORDIMAGRepository As IHCORDIMAGRepository, hCORDPRONRepository As IHCORDPRONRepository, hCORDPATORepository As IHCORDPATORepository, hCPROCTERRepository As IHCPROCTERRepository,
                   hCORDINTERepository As IHCORDINTERepository, hCHOGASINRepository As IHCHOGASINRepository, hcCONOXIGRepository As IHCCONOXIGRepository,
                   rateManualRepository As IRateManualRepository, cupsHomologation As ICupsHomologationRepository,
                   healthAdministratorRepository As IHealthAdministratorRepository, rateManualDetailRepository As IRateManualDetailRepository, ipsServiceRepository As IIPSServicesRepository,
                   billingConceptRepository As IBillingConceptRepository, hCQXREALIRepository As IHCQXREALIRepository, hCQXEQUIPRepository As IHCQXEQUIPRepository, specialityRepository As ISpecialityRepository,
                   hcHISPACARepository As IHCHISPACARepository, billingControlRepository As IBillingControlRepository, hcORHEMSERRepository As IHCORHEMSERRepository, SettingInventoryRepository As ISettingInventoryRepository,
                   ProductGroupsRepository As IProductGroupsRepository, WarehouseRepository As IWarehouseRepository, PharmaDoseRepository As IPharmaDoseRepository,
                   CupsEntityContractDescriptionsRepository As ICupsEntityContractDescriptionsRepository,
                   stayRepository As IStayRepository, hCPLAOTRPROCUPSRepository As IHCPLAOTRPROCUPSRepository,
                   serviceOrderDetailRepository As IServiceOrderDetailRepository,
                   accountControlStayRepository As IAccountControlStayRepository, aMBORDIMARepository As IAMBORDIMARepository, aMBORDLABRepository As IAMBORDLABRepository
                   )
        _stayRepository = stayRepository
        _contractServices = contractServices
        _careGroupRepository = careGroupRepository
        _cupsEntityRepository = cupsEntityRepository
        _functionalUnitRepository = functionalUnitRepository
        _billingServices = billingServices
        _accountControlStayRepository = accountControlStayRepository
        _serviceOrderDetailAdminService = serviceOrderDetailAdminService
        _patientRepository = patientRepository
        _serviceOrderDetailRepository = serviceOrderDetailRepository
        _thidPartyRepository = thidPartyRepository
        _serviceOrderAdminService = serviceOrderAdminService
        _billingSequenceAdminService = billingSequenceAdminService
        _hCORDLABORepository = hCORDLABORepository
        _costCenterRepository = costCenterRepository
        _healthProfessionalRepository = healthProfessionalRepository
        _hCORDPROQRepository = hCORDPROQRepository
        _hCQXINFORRepository = hCQXINFORRepository
        _hCORDPRONRepository = hCORDPRONRepository
        _hCPLAOTRPROCUPSRepository = hCPLAOTRPROCUPSRepository
        _hCORDPATORepository = hCORDPATORepository
        _hCORDIMAGRepository = hCORDIMAGRepository
        _hCPROCTERRepository = hCPROCTERRepository
        _hCORDINTERepository = hCORDINTERepository
        _hCHOGASINRepository = hCHOGASINRepository
        _hcCONOXIGRepository = hcCONOXIGRepository
        '_AGCITASERIPSRepository = AGCITASERIPSRepository
        _rateManualRepository = rateManualRepository
        _cupsHomologation = cupsHomologation
        _healthAdministratorRepository = healthAdministratorRepository
        _rateManualDetailRepository = rateManualDetailRepository
        _ipsServiceRepository = ipsServiceRepository
        _billingConceptRepository = billingConceptRepository
        _hCQXREALIRepository = hCQXREALIRepository
        _hCQXEQUIPRepository = hCQXEQUIPRepository
        _specialityRepository = specialityRepository
        _hcHISPACARepository = hcHISPACARepository
        _billingControlRepository = billingControlRepository
        _hcORHEMSERRepository = hcORHEMSERRepository
        _settingInventoryRepository = SettingInventoryRepository
        _productGroupsRepository = ProductGroupsRepository
        _warehouseRepository = WarehouseRepository
        _pharmaDoseRepository = PharmaDoseRepository
        _cupsEntityContractDescriptionsRepository = CupsEntityContractDescriptionsRepository
        _aMBORDIMARepository = aMBORDIMARepository
        _aMBORDLABRepository = aMBORDLABRepository
    End Sub

    ''' <summary>
    ''' Gets the homologation cups.
    ''' </summary>
    ''' <param name="parameter">The parameter.</param>
    ''' <param name="careGroupId">The care group identifier.</param>
    ''' <returns></returns>
    Private Function GetHomologationCups(parameter As String, careGroupId As Integer) As ActionResult(Of List(Of List(Of CupsHomologation))) Implements IAccountControlAdminService.GetHomologationsCups
        Try
            Dim args As Object = Utils.DeserializeJsonToObject(parameter)
            Return GetHomologationsCups(CType(args.Details, List(Of Object)), careGroupId)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of List(Of CupsHomologation))) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Resultado de validar NIT único y tercero (médico) parametrizado.
    ''' </summary>
    Private Enum ThirdPartyValidationKind
        Success = 0
        DuplicateNit = 1
        NotParametrized = 2
    End Enum

    ''' <summary>
    ''' Valida que no haya varios terceros con el mismo NIT y que el médico exista como tercero.
    ''' </summary>
    ''' <param name="nitMedico">NIT ya normalizado (p. ej. ToString().TrimStart("0")).</param>
    ''' <param name="thirdPartyOut">Tercero encontrado; Nothing si hay duplicado o no parametrizado.</param>
    ''' <param name="errorMessage">Mensaje cuando el resultado no es Success.</param>
    Private Function ValidateThirdParty(nitMedico As String, ByRef thirdPartyOut As ThirdParty, ByRef errorMessage As String) As ThirdPartyValidationKind
        thirdPartyOut = Nothing
        errorMessage = Nothing
        Dim totalThirdParties = _thidPartyRepository.ListAllThirdParty(New List(Of String) From {nitMedico}).Count()
        If totalThirdParties > 1 Then
            errorMessage = "No es posible continuar porque existe más de un tercero registrado con el mismo NIT (" & nitMedico.Trim() & ")."
            Return ThirdPartyValidationKind.DuplicateNit
        End If
        Dim tp As ThirdParty = _thidPartyRepository.GetThirdPartyByNit(nitMedico, False)
        If tp Is Nothing OrElse tp.Id = 0 Then
            errorMessage = String.Format("El Médico con Nit {0} no está parametrizado en Indigo Vie", nitMedico)
            Return ThirdPartyValidationKind.NotParametrized
        End If
        thirdPartyOut = tp
        Return ThirdPartyValidationKind.Success
    End Function

    ''' <summary>
    ''' Gets the homologations cups.
    ''' </summary>
    ''' <param name="details">The details.</param>
    ''' <param name="careGroupId">The care group identifier.</param>
    ''' <param name="IsProcedureQx">if set to <c>true</c> [is procedure qx].</param>
    ''' <returns></returns>
    Public Function GetHomologationsCups(details As List(Of Object), careGroupId As Integer, Optional IsProcedureQx As Boolean = False) As ActionResult(Of List(Of List(Of CupsHomologation)))
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


            Dim nitMedico As String = itemDetail.NitMedico.ToString().TrimStart("0")
            Dim thirdPartyMedico As ThirdParty = Nothing
            Dim tpValidationMsg As String = Nothing
            Select Case ValidateThirdParty(nitMedico, thirdPartyMedico, tpValidationMsg)
                Case ThirdPartyValidationKind.DuplicateNit
                    errors.AppendLine(tpValidationMsg)
                    Continue For
                Case ThirdPartyValidationKind.NotParametrized
                    errors.AppendLine(tpValidationMsg)
                Case ThirdPartyValidationKind.Success
            End Select

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
        ElseIf homologationMultiple OrElse IsProcedureQx Then
            If homologationMultiple Then
                Return New ActionResult(Of List(Of List(Of CupsHomologation))) With {.StateResult = False, .ObjectEmbbeded = listHomologations, .MessageResult = New List(Of String)({"MULTIPLE"})}
            Else
                Return New ActionResult(Of List(Of List(Of CupsHomologation))) With {.StateResult = False, .ObjectEmbbeded = listHomologations}
            End If
        Else
            Return New ActionResult(Of List(Of List(Of CupsHomologation))) With {.StateResult = True, .ObjectEmbbeded = listHomologations}
        End If
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

    Public Function GenerateServiceOrderMassive(objParams As String, homologations As List(Of List(Of CupsHomologation)), audit As AuditMessage) As ActionResult(Of List(Of List(Of CupsHomologation))) Implements IAccountControlAdminService.GenerateServiceOrderMassive
        Try
            Dim errors As New StringBuilder()
            Dim args As Object = Utils.DeserializeJsonToObject(objParams)
            Dim indexServiceOrderDetail As Integer = 0

            'Xml que se envia al proceso de control cuentas ambulatorio si aplica
            Dim xmlAccountControlAmbulatory As String = ""

            'Variable que me permite saber si viene desde control cuentas ambulatorio
            Dim IsAccountControlAmbulatory As Boolean = False
            If CType(args, IDictionary(Of String, Object)).ContainsKey("IsAccountControlAmbulatory") AndAlso CType(args, IDictionary(Of String, Object))("IsAccountControlAmbulatory") IsNot Nothing Then
                IsAccountControlAmbulatory = args.IsAccountControlAmbulatory
            End If

            Dim careGroup As CareGroup = _careGroupRepository.GetCareGroupById(CInt(args.CareGroupId))
            If careGroup IsNot Nothing AndAlso careGroup.Id > 0 Then
                Dim messageNotificationContract As New StringBuilder()
                If careGroup.CareGroupType = 1 Then
                    If careGroup.Contract.Status = 2 OrElse careGroup.Contract.Status = 3 Then
                        Dim messageContract = "Suspendido"
                        If careGroup.Contract.Status = 3 Then
                            messageContract = "Terminado"
                        End If
                        Return New ActionResult(Of List(Of List(Of CupsHomologation))) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = String.Format("No se puede generar la orden de servicio debido a que el contrato esta {0}", messageContract)}
                    End If
                    'EAPB con contrato
                    Dim resultContractValidation As ActionResult = ValidationsContract(careGroup.Contract)
                    If Not resultContractValidation.StateResult Then
                        Return New ActionResult(Of List(Of List(Of CupsHomologation))) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = resultContractValidation.Message}
                    End If
                    If resultContractValidation.MessageResult IsNot Nothing AndAlso resultContractValidation.MessageResult.Count > 0 Then
                        messageNotificationContract.Append(String.Join(vbCrLf, resultContractValidation.MessageResult.ToArray()))
                    End If
                End If

                Dim details = TryCast(args.Details, List(Of Object))
                'Validacion para No permitir Items a generar dentro de la orden de servicio que venga con cantidades en 0
                '========================================================================================================
                Dim resultValidate = Me.ValidateDetailsQuantityIfNegative(details)

                If resultValidate.StateResult Then
                    Return New ActionResult(Of List(Of List(Of CupsHomologation))) With {
                       .StatusCode = eStatusResult.WARNING,
                       .StateResult = False,
                       .Message = $"Los siguientes items vienen con cantidades en 0 : {resultValidate?.Message}"
                   }
                End If

                '========================================================================================================

                If homologations Is Nothing AndAlso (Not CType(args.Details, List(Of Object)).Any(Function(m) CType(m, IDictionary(Of String, Object)).ContainsKey("DatasourceType") AndAlso CType(m, IDictionary(Of String, Object))("DatasourceType") = 11)) Then
                    Dim resultHomologation As ActionResult(Of List(Of List(Of CupsHomologation))) = GetHomologationsCups(CType(args.Details, List(Of Object)), careGroup.Id, CBool(args.IsProcedureQx))
                    If resultHomologation.StateResult Then
                        resultHomologation.StatusCode = eStatusResult.SUCCESS
                        homologations = resultHomologation.ObjectEmbbeded
                    Else
                        resultHomologation.StatusCode = eStatusResult.WARNING
                        Return resultHomologation
                    End If
                End If

                Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                    'Si el proceso viene desde control cuentas ambulatorio se realiza validación o creación del ingreso
                    If IsAccountControlAmbulatory Then
                        'Si el proceso es con un ingreso nuevo se crea el xml solo con la cabecera
                        If args.IsCurrentAdmission = False Then
                            xmlAccountControlAmbulatory = GenerateXmlWithAccountControlAmbulatory(args, 0, True)
                        End If

                        'Se envia los parametros para realizar el proceso de validación
                        Dim resultStoreAmbulatory As SP_ProcessAccountControlAmbulatory_Result = _billingControlRepository.SP_ProcessAccountControlAmbulatory(args.AdmissionNumber.ToString.Trim(), args.IsCurrentAdmission, xmlAccountControlAmbulatory)
                        If resultStoreAmbulatory.CodeResult <> 0 Then 'Si el store arrojo algun error
                            scope.Dispose()
                            Return New ActionResult(Of List(Of List(Of CupsHomologation))) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = resultStoreAmbulatory.MessageResult}
                        End If

                        'Si el proceso es con un ingreso nuevo se asigna el nuevo numero de ingreso
                        If args.IsCurrentAdmission = False Then
                            args.AdmissionNumber = resultStoreAmbulatory.AdmissionNumber

                            If TryCast(args.Details, List(Of Object))?.Any() Then
                                Parallel.ForEach(TryCast(args.Details, List(Of Object)), Sub(infoDetail)
                                                                                             If CType(infoDetail, IDictionary(Of String, Object)).ContainsKey("AdmissionCode") Then
                                                                                                 infoDetail.AdmissionCode = args.AdmissionNumber
                                                                                             End If
                                                                                         End Sub)


                            End If
                        End If
                    End If

                    'Generar las órdenes de servicio
                    Dim resultSO As ActionResult(Of ServiceOrder) = New ActionResult(Of ServiceOrder)

                    If (Not CType(args.Details, List(Of Object)).Any(Function(m) CType(m, IDictionary(Of String, Object)).ContainsKey("DatasourceType") AndAlso CType(m, IDictionary(Of String, Object))("DatasourceType") = 11)) Then
                        resultSO = Me.CreateServiceOrderObject(careGroup.Id, homologations, args)
                    Else
                        resultSO = Me.CreateServiceOrderMS(careGroup.Id, args)
                    End If

                    If Not resultSO.StateResult Then
                        scope.Dispose()
                        Return New ActionResult(Of List(Of List(Of CupsHomologation))) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = resultSO.Message}
                    End If
                    'Consultar el Id de la secuencia numerica con el TAG de ordenes de servicios 755
                    Dim _idCurrentSequence As Integer = 0

                    'Guardar
                    For Each sod In resultSO.ObjectEmbbeded.ServiceOrderDetail
                        If CType(args, IDictionary(Of String, Object)).ContainsKey("HealthAdministratorId") Then
                            sod.HealthAdministratorId = Convert.ToInt32(args.HealthAdministratorId)
                        End If
                        If CType(args, IDictionary(Of String, Object)).ContainsKey("ThirdPartyId") Then
                            sod.ThirdPartyId = Convert.ToInt32(args.ThirdPartyId)
                        End If
                        If CType(args, IDictionary(Of String, Object)).ContainsKey("AuthorizationNumber") AndAlso String.IsNullOrEmpty(sod.AuthorizationNumber) Then
                            sod.AuthorizationNumber = Convert.ToString(args.AuthorizationNumber)
                        End If
                    Next
                    Dim resultSave = _serviceOrderAdminService.SaveServiceOrder(resultSO.ObjectEmbbeded, audit, _idCurrentSequence)
                    If Not resultSave.StateResult Then
                        scope.Dispose()
                        Return New ActionResult(Of List(Of List(Of CupsHomologation))) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = resultSave.Message}
                    End If

                    'Si el proceso viene desde control cuentas ambulatorio se vuelve a llamar el sp,
                    'pero esta vez dependiendo de la rejilla de donde se esten generando los detalles
                    'se crea en la tabla de control o se actualiza el parametro de orden de servicio
                    If IsAccountControlAmbulatory Then
                        'Se crea el xml que se envia
                        xmlAccountControlAmbulatory = GenerateXmlWithAccountControlAmbulatory(args, resultSave.ObjectEmbbeded.Id, False)

                        'Se envia el xml al sp que realiza el proceso de inserción de registros
                        Dim resultStoreAmbulatory As SP_ProcessAccountControlAmbulatory_Result = _billingControlRepository.SP_ProcessAccountControlAmbulatory("", args.IsCurrentAdmission, xmlAccountControlAmbulatory)
                        If resultStoreAmbulatory.CodeResult <> 0 Then 'Si el store arrojo algun error
                            scope.Dispose()
                            Return New ActionResult(Of List(Of List(Of CupsHomologation))) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = resultStoreAmbulatory.MessageResult}
                        End If
                    End If


                    If (Not CType(args.Details, List(Of Object)).Any(Function(m) CType(m, IDictionary(Of String, Object)).ContainsKey("DatasourceType") AndAlso CType(m, IDictionary(Of String, Object))("DatasourceType") = 11)) Then
                        'DatasourceType: 1 - Laboratorios, 2 - ImagenesDx, 3 - Patologías, 4 - ProcedimientosNoQx, 5 - Interconsultas, 6 - Terapias, 7 - Procedimientos de Enfermeria

                        Dim listCupsCodes = TryCast(args.Details, List(Of Object))?.Select(Function(d) d.CupsEntityCode.ToString().Trim())?.ToList()
                        Dim serviceOrderDetail = _serviceOrderDetailRepository _
                                                .GetByFilter(Function(x) x.ServiceOrderId = resultSave.ObjectEmbbeded.Id, False, {"CupsEntity"}) _
                                                .Select(Function(d) New With {d.Id, d.CUPSEntity.Code}).ToList()

                        For Each d In CType(args.Details, List(Of Object)).ToList()

                            Dim cupscode = d.CupsEntityCode.ToString().Trim()
                            Dim admisionNumber = d.AdmissionCode.ToString.Trim()
                            Dim serviceOrderDetailId As Integer? = Nothing

                            'se establece la validacion para Imagenes y laboratorios
                            If {1, 2}.Contains(CInt(d.DatasourceType)) Then
                                serviceOrderDetailId = serviceOrderDetail?.FirstOrDefault(Function(x) x.Code = cupscode)?.Id

                                If serviceOrderDetailId Is Nothing Then
                                    scope.Dispose()
                                    Throw New ArgumentNullException(NameOf(serviceOrderDetailId), $"{cupscode} Sin detalle de orden de servicio")
                                End If

                                serviceOrderDetail.RemoveAll(Function(x) x.Id = serviceOrderDetailId)
                            End If

                            Select Case CInt(d.DatasourceType)
                                Case 1
                                    ' Laboratorios
                                    Dim listAuto As List(Of Integer) = d?.Auto?.ToString.Split(",")?.ToEntityList(Of Integer)
                                    If listAuto IsNot Nothing AndAlso listAuto?.Any() Then
                                        Dim listHCORDLABO As List(Of HCORDLABO) = _hCORDLABORepository.GetByFilter(Function(x) listAuto.Contains(x.AUTO), True)
                                        For Each HCORDLABO In listHCORDLABO
                                            HCORDLABO.GENSERVICEORDER = resultSave.ObjectEmbbeded.Id
                                            HCORDLABO.IdServerOrderDetail = serviceOrderDetailId
                                            _hCORDLABORepository.SaveEntity(HCORDLABO)
                                        Next
                                    End If

                                    Dim aMBORDLAB As AMBORDLAB = _aMBORDLABRepository.GetByAdmissionNumberAndCupsCode(admisionNumber, cupscode, resultSave.ObjectEmbbeded.Id)
                                    If aMBORDLAB IsNot Nothing AndAlso aMBORDLAB.AUTO > 0 Then
                                        aMBORDLAB.GENSERVICEORDER = resultSave.ObjectEmbbeded.Id
                                        aMBORDLAB.IdServerOrderDetail = serviceOrderDetailId
                                        _aMBORDLABRepository.SaveEntity(aMBORDLAB)
                                    End If
                                Case 2
                                    ' Imagenes
                                    Dim hCORDIMAG As HCORDIMAG = _hCORDIMAGRepository.GetHCORDIMAGByAuto(CInt(d.Auto))
                                    If hCORDIMAG IsNot Nothing AndAlso hCORDIMAG.AUTO > 0 Then
                                        hCORDIMAG.GENSERVICEORDER = resultSave.ObjectEmbbeded.Id
                                        hCORDIMAG.IdServerOrderDetail = serviceOrderDetailId
                                        _hCORDIMAGRepository.SaveEntity(hCORDIMAG)
                                    End If

                                    Dim aMBORDIMA As AMBORDIMA = _aMBORDIMARepository.FindAmbordimaByAdmissionNumberAndCupsCode(admisionNumber, cupscode, resultSave.ObjectEmbbeded.Id)
                                    If aMBORDIMA IsNot Nothing AndAlso aMBORDIMA.AUTO > 0 Then
                                        aMBORDIMA.GENSERVICEORDER = resultSave.ObjectEmbbeded.Id
                                        aMBORDIMA.IdServerOrderDetail = serviceOrderDetailId
                                        _aMBORDIMARepository.SaveEntity(aMBORDIMA)
                                    End If

                                Case 3
                                    Dim hCORDPATO As HCORDPATO = _hCORDPATORepository.GetHCORDPATOByAuto(CInt(d.Auto))
                                    If hCORDPATO IsNot Nothing AndAlso hCORDPATO.AUTO > 0 Then
                                        hCORDPATO.GENSERVICEORDER = resultSave.ObjectEmbbeded.Id
                                        _hCORDPATORepository.SaveEntity(hCORDPATO)
                                    End If
                                Case 4
                                    If CType(d, IDictionary(Of String, Object)).ContainsKey("EntityName") AndAlso CType(d, IDictionary(Of String, Object))("EntityName") = "HCPLAOTRPROCUPS" Then
                                        Dim hCPLAOTRPROCUPS As HCPLAOTRPROCUPS = _hCPLAOTRPROCUPSRepository.GetHCPLAOTRPROCUPSByID(CInt(d.Auto))
                                        If hCPLAOTRPROCUPS IsNot Nothing AndAlso hCPLAOTRPROCUPS.ID > 0 Then
                                            hCPLAOTRPROCUPS.GENSERVICEORDER = resultSave.ObjectEmbbeded.Id
                                            _hCPLAOTRPROCUPSRepository.SaveEntity(hCPLAOTRPROCUPS)
                                        End If
                                    Else
                                        Dim hCORDPRON As HCORDPRON = _hCORDPRONRepository.GetHCORDPRONByAuto(CInt(d.Auto))
                                        If hCORDPRON IsNot Nothing AndAlso hCORDPRON.AUTO > 0 Then
                                            hCORDPRON.GENSERVICEORDER = resultSave.ObjectEmbbeded.Id
                                            _hCORDPRONRepository.SaveEntity(hCORDPRON)
                                        End If
                                    End If
                                Case 5
                                    ' Interconsultas - HCORDINTE
                                    Dim hCORDINTE As HCORDINTE = _hCORDINTERepository.GetHCORDINTEByAuto(CInt(d.Auto))
                                    If hCORDINTE IsNot Nothing AndAlso hCORDINTE.AUTO > 0 Then
                                        hCORDINTE.GENSERVICEORDER = resultSave.ObjectEmbbeded.Id
                                        _hCORDINTERepository.SaveEntity(hCORDINTE)

                                        ' Si tiene folio realizado, actualizar también HCHISPACA relacionado
                                        If hCORDINTE.NUMFOLINT IsNot Nothing Then
                                            Dim hCHISPACARelacionado As HCHISPACA = _hcHISPACARepository.GetByFilter(
                                                Function(x) x.IPCODPACI = hCORDINTE.IPCODPACI _
                                                    AndAlso x.NUMINGRES = hCORDINTE.NUMINGRES _
                                                    AndAlso x.NUMEFOLIO = hCORDINTE.NUMFOLINT,
                                                True
                                            ).FirstOrDefault()

                                            If hCHISPACARelacionado IsNot Nothing AndAlso hCHISPACARelacionado.ID > 0 Then
                                                hCHISPACARelacionado.GENSERVICEORDER = resultSave.ObjectEmbbeded.Id
                                                _hcHISPACARepository.SaveEntity(hCHISPACARelacionado)
                                            End If
                                        End If
                                    End If
                                Case 6
                                    Dim hCPROCTER As HCPROCTER = _hCPROCTERRepository.GetHCPROCTERByAuto(CInt(d.Auto))
                                    If hCPROCTER IsNot Nothing AndAlso hCPROCTER.CODCONSEC > 0 Then
                                        hCPROCTER.GENSERVICEORDER = resultSave.ObjectEmbbeded.Id
                                        _hCPROCTERRepository.SaveEntity(hCPROCTER)
                                    End If
                                Case 7
                                    Dim listAuto As List(Of Integer) = d?.Auto?.ToString.Split(",")?.ToEntityList(Of Integer)
                                    If listAuto?.Any() Then
                                        Dim listHCHOGASIN As List(Of HCHOGASIN) = _hCHOGASINRepository.GetByFilter(Function(x) listAuto.Contains(x.CONSECUTI), True) '_hCHOGASINRepository.GetHCHOGASINByAuto(CInt(d.Auto))
                                        For Each HCHOGASIN In listHCHOGASIN
                                            If HCHOGASIN IsNot Nothing AndAlso HCHOGASIN.CONSECUTI > 0 Then
                                                HCHOGASIN.GENSERVICEORDER = resultSave.ObjectEmbbeded.Id
                                                _hCHOGASINRepository.SaveEntity(HCHOGASIN)
                                            End If
                                        Next
                                    End If
                                Case 8
                                    Dim hCCONOXIG As HCCONOXIG = _hcCONOXIGRepository.GetHCCONOXIGByIDCONOXIG(CInt(d.Auto))
                                    If hCCONOXIG IsNot Nothing AndAlso hCCONOXIG.IDCONOXIG > 0 Then
                                        hCCONOXIG.GENSERVICEORDER = resultSave.ObjectEmbbeded.Id
                                        _hcCONOXIGRepository.SaveEntity(hCCONOXIG)
                                    End If
                                Case 9
                                    ' Valoraciones - HCHISPACA
                                    Dim hCHISPACA As HCHISPACA = _hcHISPACARepository.GetHCHISPACAByID(CInt(d.Auto))
                                    If hCHISPACA IsNot Nothing AndAlso hCHISPACA.ID > 0 Then
                                        hCHISPACA.GENSERVICEORDER = resultSave.ObjectEmbbeded.Id
                                        _hcHISPACARepository.SaveEntity(hCHISPACA)

                                        ' Actualizar también HCORDINTE relacionado (donde NUMFOLINT = NUMEFOLIO de HCHISPACA)
                                        Dim hCORDINTERelacionado As HCORDINTE = _hCORDINTERepository.GetByFilter(
                                            Function(x) x.IPCODPACI = hCHISPACA.IPCODPACI _
                                                AndAlso x.NUMINGRES = hCHISPACA.NUMINGRES _
                                                AndAlso x.NUMFOLINT = hCHISPACA.NUMEFOLIO,
                                            True
                                        ).FirstOrDefault()

                                        If hCORDINTERelacionado IsNot Nothing AndAlso hCORDINTERelacionado.AUTO > 0 Then
                                            hCORDINTERelacionado.GENSERVICEORDER = resultSave.ObjectEmbbeded.Id
                                            _hCORDINTERepository.SaveEntity(hCORDINTERelacionado)
                                        End If
                                    End If
                                Case 10
                                    Dim hCORHEMSER As HCORHEMSER = _hcORHEMSERRepository.GetHCORHEMSERByID(CInt(d.Auto))
                                    If hCORHEMSER IsNot Nothing AndAlso hCORHEMSER.ID > 0 Then
                                        hCORHEMSER.ORDSERVICIOID = resultSave.ObjectEmbbeded.Id
                                        hCORHEMSER.GENORDSER = True
                                        _hcORHEMSERRepository.SaveEntity(hCORHEMSER)
                                    End If
                                Case 12
                                    ' consultamos la estancia
                                    Dim stayId = Integer.Parse(d.Auto)
                                    Dim stay = _stayRepository.FirstOrDefault(Function(m) m.ID = stayId, includes:={"CHREGESTADET", "ADINGRESO"})

                                    If stay IsNot Nothing Then

                                        Dim ostay = JsonConvert.DeserializeObject(Of CHREGESTA)(d.Stay.ToString())
                                        Dim endDate = CType(d.EndDate, Date)
                                        Dim startDate = CType(d.InitialDate, Date)
                                        Dim toleranceInSeconds As Integer = 1 ' si la diferencia es mayor o igual, dejar hasta segundos 
                                        Dim accountStay = _accountControlStayRepository.FirstOrDefault(Function(m) m.StayId = ostay.ID _
                                                                                                        AndAlso Entity.DbFunctions.DiffSeconds(m.StartDate, startDate) <= toleranceInSeconds _
                                                                                                        AndAlso m.EndDate = endDate)

                                        If accountStay IsNot Nothing Then
                                            accountStay.LiquidationDate = Date.Now
                                            accountStay.ServiceOrderDetailId = resultSave.ObjectEmbbeded.Id
                                            accountStay.MarkAsModified()

                                            _accountControlStayRepository.SaveEntity(accountStay)
                                            _accountControlStayRepository.UnitWork.Commit()
                                        End If

                                        If stay.ADINGRESO.GENULTLIQUI Is Nothing OrElse stay.ADINGRESO.GENULTLIQUI < CType(d.EndDate, Date) Then
                                            stay.ADINGRESO.GENULTLIQUI = CType(d.EndDate, Date)
                                        End If
                                        Dim cupsId As Integer = CInt(d.CupsId)

                                        If stay.CHREGESTADET.Any(Function(det) det.GENCUPSLIQ = cupsId) Then
                                            Dim det = stay.CHREGESTADET.FirstOrDefault(Function(m) m.GENCUPSLIQ = cupsId)
                                            det.CANTIDADLIQ += 1
                                            det.GENULTFEC = stay.ADINGRESO.GENULTLIQUI 'lastLiquidateDate
                                        Else
                                            Dim estnDet As New CHREGESTADET() With {
                                                .GENLIQUIDA = CType(d.EndDate, Date),
                                                .GENULTFEC = stay.ADINGRESO.GENULTLIQUI,
                                                .GENCUPSLIQ = cupsId,
                                                .CUPSEntityContractDescriptionId = ostay.CUPSEntityContractDescriptionId,
                                                .CANTIDADLIQ = 1,
                                                .Liquidated = False
                                            }

                                            stay.CHREGESTADET.Add(estnDet)
                                        End If

                                        stay.GENESTLIQ = ostay.GENESTLIQ

                                        Dim estUnluidate = stay.CHREGESTADET.FirstOrDefault()
                                        _stayRepository.SaveEntity(stay)
                                        _stayRepository.UnitWork.Commit()

                                        Dim serviceOrder = resultSO.ObjectEmbbeded.ServiceOrderDetail.FirstOrDefault(Function(m) m.HospitalStayId = stay.ID)
                                        If serviceOrder IsNot Nothing Then
                                            serviceOrder.HospitalStayDetailId = estUnluidate.ID
                                        End If
                                    End If
                            End Select
                        Next


                        _hCORDLABORepository.UnitWork.Commit()
                        _hCORDIMAGRepository.UnitWork.Commit()
                        _hCORDPATORepository.UnitWork.Commit()
                        _hCORDPRONRepository.UnitWork.Commit()
                        _hCPLAOTRPROCUPSRepository.UnitWork.Commit()
                        _hCPROCTERRepository.UnitWork.Commit()
                        _hCORDINTERepository.UnitWork.Commit()
                        _hCHOGASINRepository.UnitWork.Commit()
                        _hcCONOXIGRepository.UnitWork.Commit()
                        _hcHISPACARepository.UnitWork.Commit()
                        _hcORHEMSERRepository.UnitWork.Commit()
                        _aMBORDIMARepository.UnitWork.Commit()
                        _aMBORDLABRepository.UnitWork.Commit()

                    ElseIf CType(args, IDictionary(Of String, Object)).ContainsKey("ListIdDose") AndAlso CType(args.ListIdDose, List(Of Object)).Count > 0 Then
                        Dim ListGroupingCodeDose = New List(Of Guid)
                        CType(args.ListIdDose, List(Of Object)).ForEach(Sub(c)
                                                                            Dim GuidX = New Guid(c.ToString())
                                                                            ListGroupingCodeDose.Add(GuidX)
                                                                        End Sub)
                        Dim Result = _pharmaDoseRepository.GetByFilter(Function(x) ListGroupingCodeDose.Contains(x.GroupingCodeDose)).ToList()
                        Result.ForEach(Sub(x)
                                           x.DeliveryStatus = 2
                                           _pharmaDoseRepository.SaveEntity(x)
                                       End Sub)
                        _pharmaDoseRepository.UnitWork.Commit()
                    End If



#If DEBUG Then
                    scope.Complete()
#Else
                                                            scope.Complete()
#End If
                    Return New ActionResult(Of List(Of List(Of CupsHomologation))) With {.StatusCode = eStatusResult.SUCCESS, .StateResult = True, .Message = resultSave.ObjectEmbbeded.Code, .MessageResult = {messageNotificationContract.ToString(), resultSave.ObjectEmbbeded.AdmissionNumber}.ToList()}
                End Using

            Else
                errors.AppendLine("No se encontró grupo de atención para la admisión seleccionada")
            End If
            If errors.Length > 0 Then
                Return New ActionResult(Of List(Of List(Of CupsHomologation))) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = errors.ToString()}
            End If
            Return New ActionResult(Of List(Of List(Of CupsHomologation))) With {.StatusCode = eStatusResult.SUCCESS, .StateResult = True}
        Catch ex As IndigoValidationException
            Return New ActionResult(Of List(Of List(Of CupsHomologation))) With {.StatusCode = eStatusResult.WARNING, .StateResult = False, .Message = ex.Message}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of List(Of CupsHomologation))) With {.StatusCode = eStatusResult.EXCEPTION, .StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Valida que las cantidades sean mayores a 0 dependiento del tipo de proceso que se este llevando a cabo
    ''' </summary>
    ''' <param name="details"></param>
    ''' <returns></returns>
    Private Function ValidateDetailsQuantityIfNegative(details As List(Of Object)) As ActionResult
        Try
            'se valida si existen cantidades negativas
            If details?.Any(Function(x) InvalidQuantityByDic(TryCast(x, IDictionary(Of String, Object)))) Then

                Dim message = String.Join(", ", details _
                                            .FindAll(Function(x) InvalidQuantityByDic(TryCast(x, IDictionary(Of String, Object)))) _
                                            .Select(Function(s)
                                                        Dim dict = TryCast(s, IDictionary(Of String, Object))
                                                        Dim description As String = String.Empty

                                                        Select Case CInt(dict("DatasourceType"))
                                                            Case 11
                                                                description = dict("ProductCodeName")?.ToString()
                                                            Case Else
                                                                If dict.ContainsKey("CupsDescription") Then
                                                                    description = dict("CupsDescription")?.ToString()

                                                                ElseIf dict.ContainsKey("CupsEntityCode") Then
                                                                    description = dict("CupsEntityCode")?.ToString()
                                                                End If
                                                        End Select
                                                        Return Trim(description)
                                                    End Function))

                Return New ActionResult With {
                    .StatusCode = eStatusResult.WARNING,
                    .StateResult = True,
                    .Message = $"Los siguientes items vienen con cantidades en 0 : {message}"
                }
            End If

            Return New ActionResult With {.StateResult = False, .Message = "No se encontraron cantidades negativas ni iguales a cero."}
        Catch ex As Exception
            Return New ActionResult With {.StateResult = True, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Identifica el tipo de proceso que se esta llevando a cabo, para validar las cantidades en 0
    ''' </summary>
    Function InvalidQuantityByDic(item As IDictionary(Of String, Object)) As Boolean
        If item Is Nothing Then Return False

        ':Proceso: Todos los demas
        If item.ContainsKey("DatasourceType") AndAlso Convert.ToInt32(item("DatasourceType")) <> 11 Then
            If item.ContainsKey("Quantity") AndAlso Convert.ToInt32(item("Quantity")) <= 0 Then
                Return True
            End If
        End If

        'Proceso:Central de mezclas
        If item.ContainsKey("DatasourceType") AndAlso Convert.ToInt32(item("DatasourceType")) = 11 Then
            If item.ContainsKey("TotalQuantities") AndAlso Convert.ToInt32(item("TotalQuantities")) <= 0 Then
                Return True
            End If
        End If

        Return False
    End Function

    ''' <summary>
    ''' Crea el xml que se envia al sp del proceso de control cuentas ambulatorio
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateXmlWithAccountControlAmbulatory(args As Object, serviceOrderId As Integer, isOnlyHeader As Boolean) As String
        Dim builder As StringBuilder = New StringBuilder()

        builder.Append("<Header>")
        If isOnlyHeader Then 'Si solo se arma el xml con la cabecera es porque se esta creando un nuevo ingreso
            builder.Append("<IPCODPACI>" & args.NewAdmission.IPCODPACI & "</IPCODPACI>")
            builder.Append("<ITIPORIES>" & args.NewAdmission.ITIPORIES & "</ITIPORIES>")
            builder.Append("<ICAUSAING>" & args.NewAdmission.ICAUSAING & "</ICAUSAING>")
            builder.Append("<CODENTIDA>" & args.NewAdmission.CODENTIDA & "</CODENTIDA>")
            builder.Append("<CODCENATE>" & args.NewAdmission.CODCENATE & "</CODCENATE>")
            builder.Append("<GENCAREGROUP>" & args.NewAdmission.GENCAREGROUP & "</GENCAREGROUP>")
            builder.Append("<GENCONENTITY>" & args.NewAdmission.GENCONENTITY & "</GENCONENTITY>")
            builder.Append("<CODUSUCRE>" & args.NewAdmission.CODUSUCRE & "</CODUSUCRE>")
            builder.Append("<ISOATVALO>" & args.NewAdmission.ISOATVALO & "</ISOATVALO>")
            builder.Append("<ISALCODIG>" & args.NewAdmission.ISALCODIG & "</ISALCODIG>")
            builder.Append("<IOBSERVAC>" & args.NewAdmission.IOBSERVAC & "</IOBSERVAC>")
            builder.Append("<UFUCODIGO>" & args.NewAdmission.UFUCODIGO & "</UFUCODIGO>")
            builder.Append("<IAUTORIZA>" & args.NewAdmission.IAUTORIZA & "</IAUTORIZA>")

            If args.NewAdmission.IdEntryRoutesHealthServices IsNot Nothing Then
                builder.Append("<IdEntryRoutesHealthServices>" & args.NewAdmission.IdEntryRoutesHealthServices & "</IdEntryRoutesHealthServices>")
            End If

            If args.NewAdmission.IdHealthPurposes IsNot Nothing Then
                builder.Append("<IdHealthPurposes>" & args.NewAdmission.IdHealthPurposes & "</IdHealthPurposes>")
            End If

            If args.NewAdmission.IdAdmissionModalities IsNot Nothing Then
                builder.Append("<IdAdmissionModalities>" & args.NewAdmission.IdAdmissionModalities & "</IdAdmissionModalities>")
            End If

        Else 'Si se arma el xml solo con los detalles es porque ya paso el proceso de validación o creación de ingreso y va a realizar la lógica para el check en la rejilla
            'Se envia al sp la identificación de la rejilla desde donde se estan generando los detalles
            builder.Append("<TypeGrid>" & args.TypeGrid & "</TypeGrid>")

            For Each infoDetail As Object In CType(args.Details, List(Of Object)).ToList()
                builder.Append("<Details>")
                builder.Append("<AdmissionNumber>" & infoDetail.AdmissionCode & "</AdmissionNumber>")
                builder.Append("<Folio>" & infoDetail.FolioNumber & "</Folio>")

                'Este campo se envia cuando se generan los detalles desde la rejilla de procedimientos odontologicos
                If CType(infoDetail, IDictionary(Of String, Object)).ContainsKey("ProcedureCode") Then
                    builder.Append("<ProcedureCode>" & infoDetail.ProcedureCode & "</ProcedureCode>")
                Else
                    builder.Append("<ProcedureCode>" & 0 & "</ProcedureCode>")
                End If

                'Este campo se envia cuando se generan los detalles desde una rejilla diferente a la de procedimientos odontologicos
                If CType(infoDetail, IDictionary(Of String, Object)).ContainsKey("EntityId") Then
                    builder.Append("<EntityId>" & infoDetail.EntityId & "</EntityId>")
                Else
                    builder.Append("<EntityId>" & 0 & "</EntityId>")
                End If

                builder.Append("<ServiceOrderId>" & serviceOrderId & "</ServiceOrderId>")
                builder.Append("<CareCenterCode>" & infoDetail.CareCenterCode & "</CareCenterCode>")
                builder.Append("<FunctionalUnitCode>" & infoDetail.FunctionalUnitCode & "</FunctionalUnitCode>")
                builder.Append("<PatientCode>" & infoDetail.PatientCode & "</PatientCode>")
                builder.Append("<ProfessionalCode>" & infoDetail.ProfessionalCode & "</ProfessionalCode>")
                builder.Append("<CupsEntityCode>" & infoDetail.CupsEntityCode & "</CupsEntityCode>")
                builder.Append("<Quantity>" & infoDetail.Quantity & "</Quantity>")
                builder.Append("<Date>" & Convert.ToDateTime(infoDetail.Date).ToString("yyyy-MM-dd HH:mm:ss") & "</Date>")
                builder.Append("</Details>")
            Next
        End If
        builder.Append("</Header>")

        Return builder.ToString()
    End Function

    ''' <summary>
    ''' Gets the service order detail homologation.
    ''' </summary>
    ''' <param name="careGroupId">The care group identifier.</param>
    ''' <param name="listHomologations">The list homologations.</param>
    ''' <returns></returns>

    Public Function GetServiceOrderDetailHomologation(careGroupId As Integer, listHomologations As List(Of List(Of CupsHomologation)), objParams As String) As ActionResult(Of ServiceOrder) Implements IAccountControlAdminService.GetServiceOrderDetailHomologation
        Try
            Dim args As Object = Utils.DeserializeJsonToObject(objParams)
            Return CreateServiceOrderObject(careGroupId, listHomologations, args)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of ServiceOrder) With {.StateResult = False, .Message = ResourceManager.GetString("ErrorUnknown")}
        End Try
    End Function

    ''' <summary>
    ''' Generates the service order massive with list detail.
    ''' </summary>
    ''' <param name="parameters">The parameters.</param>
    ''' <param name="listServiceOrderDetail">The list service order detail.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function GenerateServiceOrderMassiveWithListDetail(parameters As String, listServiceOrderDetail As List(Of ServiceOrderDetail), audit As AuditMessage) As ActionResult Implements IAccountControlAdminService.GenerateServiceOrderMassiveWithListDetail
        Try

            Dim errors As New StringBuilder()
            Dim args As Object = Utils.DeserializeJsonToObject(parameters)
            Dim cupsEntity As CUPSEntity = Nothing
            Dim functionalUnit As Domain.Payroll.Entities.FunctionalUnit = Nothing
            Dim listHomologations As New List(Of List(Of CupsHomologation))()

            Dim careGroup As CareGroup = _careGroupRepository.GetCareGroupById(CInt(args.CareGroupId))
            Dim messageNotificationContract As New StringBuilder()

            Using scope As New TransactionScope(TransactionScopeOption.Required, New TransactionOptions() With {.Timeout = TransactionManager.MaximumTimeout, .IsolationLevel = IsolationLevel.ReadCommitted})

                'valida que el profesional que realizo no este repetido
                If listServiceOrderDetail.Any(Function(x) x.ServiceOrderDetailSurgical.Any()) Then

                    Parallel.ForEach(listServiceOrderDetail.ToList(), Sub(Item As ServiceOrderDetail)
                                                                          Dim ClassService As String = Item.ServiceOrderDetailSurgical.ToList().Find(Function(x) x.ClassServiceIps <> ResourceManager.GetString("Surgeon", "Contract") AndAlso x.PerformsHealthProfessionalCode = Item.PerformsHealthProfessionalCode)?.ClassServiceIps
                                                                          If Not String.IsNullOrEmpty(ClassService) Then
                                                                              errors.AppendLine(ClassService)
                                                                          End If
                                                                      End Sub)

                    If errors.Length > 0 Then
                        Return New ActionResult With {.StateResult = False, .Message = String.Format("El profesional que realizó esta también como: {0}", errors.ToString)}
                    End If
                End If

                If careGroup.CareGroupType = 1 Then
                    If careGroup.Contract.Status = 2 OrElse careGroup.Contract.Status = 3 Then
                        Dim messageContract = "Suspendido"
                        If careGroup.Contract.Status = 3 Then
                            messageContract = "Terminado"
                        End If
                        Return New ActionResult With {.StateResult = False, .Message = String.Format("No se puede liquidar el folio {0} debido a que el contrato esta {1}", messageContract)}
                    End If
                    'EAPB con contrato
                    Dim resultContractValidation As ActionResult = ValidationsContract(careGroup.Contract)
                    If resultContractValidation.MessageResult IsNot Nothing AndAlso resultContractValidation.MessageResult.Count > 0 Then
                        messageNotificationContract.Append(String.Join(vbCrLf, resultContractValidation.MessageResult.ToArray()))
                    End If
                End If

                'Generar las órdenes de servicio

                Dim serviceOrder As New ServiceOrder()
                With serviceOrder
                    .Code = String.Empty
                    .AdmissionNumber = args.AdmissionNumber.ToString().Trim()
                    .PatientCode = args.PatientCode.ToString().Trim()
                    .OrderDate = Date.Now
                    .OperatingUnitId = args.OperativeUnitId
                    .EntityName = args.EntityName
                    .Status = 1
                End With

                listServiceOrderDetail.ForEach(Sub(x)
                                                   serviceOrder.ServiceOrderDetail.Add(x)
                                               End Sub)

                'Consultar el Id de la secuencia numerica con el TAG de ordenes de servicios 755
                Dim _idCurrentSequence As Integer = 0
                'Dim _sequence As BillingSequence = _billingSequenceAdminService.GetSequenseByIdForm("755")
                'If _sequence IsNot Nothing AndAlso _sequence.Id > 0 Then
                '    If _sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                '        _idCurrentSequence = _sequence.BillingSequenceDetail(0).Id
                '    ElseIf _sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                '        Dim res = (From ou As BillingSequenceDetail In _sequence.BillingSequenceDetail Where ou.OperatingUnit.Id = CInt(args.OperativeUnitId) Select ou).ToList()
                '        If res IsNot Nothing AndAlso res.Count > 0 Then
                '            _idCurrentSequence = res(0).Id
                '        End If
                '    End If
                'End If

                'Guardar
                Dim resultSave = _serviceOrderAdminService.SaveServiceOrder(serviceOrder, audit, _idCurrentSequence)
                If Not resultSave.StateResult Then
                    Return New ActionResult With {.StateResult = False, .Message = resultSave.Message}
                End If
                For Each d In CType(args.Details, List(Of Object)).ToList()
                    Dim Auto = CInt(d.auto)
                    If CBool(d.ReportQx) Then
                        Dim hCQXREALI As HCQXREALI = _hCQXREALIRepository.GetByFilter(Function(s) s.CONSECUQX = Auto).FirstOrDefault
                        If hCQXREALI IsNot Nothing AndAlso hCQXREALI.CONSECUQX > 0 Then
                            'se agrega validacion para Procedimientos Bilaterales, si el ya tiene una orden de servicio y es de tipo bilateral entonces guarde el serviceOrderId en GENSERVICEORDER2
                            If hCQXREALI.GENSERVICEORDER Is Nothing Then
                                hCQXREALI.GENSERVICEORDER = resultSave.ObjectEmbbeded.Id
                            ElseIf hCQXREALI.CODVIAABO = "02" Then
                                hCQXREALI.GENSERVICEORDER2 = resultSave.ObjectEmbbeded.Id
                            End If
                            _hCQXREALIRepository.SaveEntity(hCQXREALI)
                        End If
                    Else
                        Dim hCORDLABO As HCORDPROQ = _hCORDPROQRepository.GetHCORDPROQByAuto(CInt(d.Auto), True)
                        If hCORDLABO IsNot Nothing AndAlso hCORDLABO.AUTO > 0 Then
                            hCORDLABO.GENSERVICEORDER = resultSave.ObjectEmbbeded.Id
                            _hCORDPROQRepository.SaveEntity(hCORDLABO)
                        End If
                    End If
                Next
                _hCQXINFORRepository.UnitWork.Commit()
                _hCORDPROQRepository.UnitWork.Commit()

                scope.Complete()
                Return New ActionResult With {.StateResult = True, .Message = resultSave.ObjectEmbbeded.Code, .MessageResult = {messageNotificationContract.ToString()}.ToList()}
            End Using
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult With {.StateResult = False, .Message = IndigoManagementExceptions.GetExceptionDetails(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Creates the service order object.
    ''' </summary>
    ''' <param name="careGroupId">The care group identifier.</param>
    ''' <param name="listHomologations">The list homologations.</param>
    ''' <param name="args">The arguments.</param>
    ''' <returns></returns>
    Private Function CreateServiceOrderObject(careGroupId As Integer, listHomologations As List(Of List(Of CupsHomologation)), args As Object) As ActionResult(Of ServiceOrder)
        Try
            Dim CareCenterCode As String = Nothing
            Dim serviceOrder As New ServiceOrder()
            Dim ThirdPartyPatientId As Integer = 0

            With serviceOrder
                .Code = String.Empty
                If CType(args, IDictionary(Of String, Object))("AdmissionNumber") IsNot Nothing Then
                    .AdmissionNumber = args.AdmissionNumber.ToString().Trim()
                End If
                If CType(args, IDictionary(Of String, Object))("CareCenterCode") IsNot Nothing Then
                    CareCenterCode = args.CareCenterCode.ToString().Trim()
                End If
                If CType(args, IDictionary(Of String, Object))("PatientCode") IsNot Nothing Then
                    .PatientCode = args.PatientCode.ToString().Trim()
                    Dim thirdparty = _thidPartyRepository.FirstOrDefault(Function(x) x.Nit = .PatientCode, False)

                    If thirdparty Is Nothing Then Throw New IndigoValidationException($"El paciente con código {serviceOrder.PatientCode} no existe como tercero.")

                    ThirdPartyPatientId = thirdparty.Id
                End If
                .OrderDate = Date.Now
                If CType(args, IDictionary(Of String, Object))("OperativeUnitId") IsNot Nothing Then
                    .OperatingUnitId = args.OperativeUnitId
                End If
                If CType(args, IDictionary(Of String, Object)).ContainsKey("EntityName") Then
                    .EntityName = "ControlOutPatientServices"
                Else
                    .EntityName = "AccountControl"
                End If
                .EntityCode = ""
                .EntityId = 0
                .Status = 1
            End With

            Dim patient As INPACIENT = _patientRepository.GetOnlyPatientByIdentification(args.PatientCode.ToString().Trim(), False)
            Dim healthAdministratorPatient As HealthAdministrator = Nothing
            If patient.GENCONENTITY IsNot Nothing AndAlso patient.GENCONENTITY > 0 Then
                healthAdministratorPatient = _healthAdministratorRepository.GetHealthAdministratorById(patient.GENCONENTITY)
            End If
            Dim careGroup As CareGroup = _careGroupRepository.GetCareGroupById(careGroupId)

            Dim serviceOrderDetail As ServiceOrderDetail = Nothing
            'Dim surgeryNumber As Integer = 1
            Dim IsBasic As Boolean = False

            Dim validation As New StringBuilder()
            Dim Identity As Integer = 0
            For Each d As Object In CType(args.Details, List(Of Object)).ToList()
                Identity += 1
                Dim QuantitySerialService As Integer = 1
                Dim cupsEntity As CUPSEntity = _cupsEntityRepository.GetCupsEntity(d.CupsEntityCode.ToString().Trim())
                If cupsEntity.ServiceType = 1 AndAlso cupsEntity?.SerialService AndAlso CType(d, IDictionary(Of String, Object)).ContainsKey("Quantity") AndAlso d.Quantity > 1 Then
                    QuantitySerialService = d.Quantity
                    d.Quantity = 1
                End If
                For i = 1 To QuantitySerialService

                    Dim functionalUnit As Domain.Payroll.Entities.FunctionalUnit = _functionalUnitRepository.GetFunctionalUnit(d.FunctionalUnitCode.ToString().Trim())

                    Dim homologation = listHomologations.Find(Function(e) e(0).CupsEntityId = cupsEntity.Id)

                    If d.NitMedico Is Nothing OrElse d.ProfessionalSpecialistCode = "999" OrElse d.ProfessionalCode = "999" Then
                        If CType(d, IDictionary(Of String, Object)).ContainsKey("TipoSolicitud") AndAlso d.TipoSolicitud IsNot Nothing AndAlso d.TipoSolicitud <> 1 Then
                            Dim specialty = _specialityRepository.GetSpecialityByCode("999")
                            If specialty Is Nothing OrElse String.IsNullOrEmpty(specialty.CODESPECI) Then
                                Return New ActionResult(Of ServiceOrder) With {.StateResult = False, .Message = "No existe especialidad con código 999 en Indigo Vie Cloud Platform"}
                            End If
                            d.NitMedico = "999"
                            d.ProfessionalSpecialistCode = "999"
                            d.ProfessionalCode = "999"
                        End If
                    End If

                    If Not CType(d, IDictionary(Of String, Object)).ContainsKey("ProfessionalCode") OrElse d.ProfessionalCode Is Nothing _
                        OrElse d.ProfessionalCode.ToString().Trim().Equals("") Then
                        validation.AppendLine($"No se seleccionó el Medico que realizó para el CUPS {d.CupsEntityCode}")
                        Continue For
                    End If

                    If Not CType(d, IDictionary(Of String, Object)).ContainsKey("Date") OrElse d.Date Is Nothing _
                        OrElse CType(d.Date, Date) = New Date() Then
                        validation.AppendLine($"No se seleccionó la fecha de realización para el CUPS {d.CupsEntityCode}")
                        Continue For
                    End If

                    Dim medicoRealizo As INPROFSAL = _healthProfessionalRepository.GetHealthProfessionalByCode(d.ProfessionalCode)
                    If medicoRealizo IsNot Nothing AndAlso medicoRealizo.CODIGONIT IsNot Nothing Then
                        d.NitMedico = medicoRealizo.CODIGONIT

                        'Se realiza esta validación por el bug 1941, ya que al crear la orden de servicios desde control de servicios ambulatorios estaba tomando siempre la primera especialidad y no la que uno escoge al momento de crear la cita
                        If (Not CType(d, IDictionary(Of String, Object)).ContainsKey("ProfessionalSpecialistCode")) OrElse String.IsNullOrEmpty(d.ProfessionalSpecialistCode) Then
                            d.ProfessionalSpecialistCode = medicoRealizo.CODESPEC1
                        End If
                    End If

                    'Validar que el medico este creado como tercero
                    Dim nitMedico As String = d.NitMedico.ToString().TrimStart("0")
                    Dim thirdPartyMedico As ThirdParty = Nothing
                    Dim tpValidationMsg As String = Nothing
                    Dim tpKind = ValidateThirdParty(nitMedico, thirdPartyMedico, tpValidationMsg)
                    If tpKind <> ThirdPartyValidationKind.Success Then
                        Return New ActionResult(Of ServiceOrder) With {.StateResult = False, .Message = tpValidationMsg}
                    End If

                    Dim RiasId As Integer? = Nothing
                    If CType(d, IDictionary(Of String, Object)).ContainsKey("RiasId") AndAlso CType(d, IDictionary(Of String, Object))("RiasId") IsNot Nothing Then
                        RiasId = CInt(d.RiasId)
                    End If

                    Dim ContractDescriptionId As Integer? = Nothing
                    If CType(d, IDictionary(Of String, Object)).ContainsKey("ContractDescriptionId") AndAlso CType(d, IDictionary(Of String, Object))("ContractDescriptionId") IsNot Nothing Then
                        ContractDescriptionId = CInt(d.ContractDescriptionId)
                    End If

                    'Si viene este campo en los detalles es porque el detalle de la orden de servicio se arma con la cotización
                    If CType(d, IDictionary(Of String, Object)).ContainsKey("QuotationServiceOrderDetailId") AndAlso CType(d, IDictionary(Of String, Object))("QuotationServiceOrderDetailId") IsNot Nothing Then
                        serviceOrderDetail = GetServiceOrderDetailByQuotation(CInt(d.QuotationServiceOrderDetailId), CType(d.Date, Date), functionalUnit.Id, d.ProfessionalSpecialistCode.ToString().Trim(), d.ProfessionalCode.ToString().Trim(), thirdPartyMedico.Id)
                    Else 'Si no viene este campo el detalle de la orden se realiza con el proceso normal
                        Dim result = _serviceOrderDetailAdminService.GetValueServiceWithRefactorValue(serviceOrder.AdmissionNumber, CareCenterCode, homologation, careGroupId, functionalUnit.Id, d.ProfessionalSpecialistCode.ToString().Trim(), CType(d.Date, Date), patient.IPSEXOPAC, patient.IPFECNACI, d.Quantity, d.ProfessionalCode.ToString().Trim(), thirdPartyMedico.Id, RiasId, ContractDescriptionId)
                        If Not result.StateResult Then
                            Return New ActionResult(Of ServiceOrder) With {.StateResult = False, .Message = result.Message}
                        End If

                        serviceOrderDetail = result.ObjectEmbbeded(0)

                        serviceOrderDetail.CodeNameCareGroup = String.Concat(careGroup.Code, " - ", careGroup.Name)
                        Select Case careGroup.CareGroupType
                            Case 1 ' EAPB Con contrato
                                If careGroup.ContractId IsNot Nothing Then
                                    serviceOrderDetail.ThirdPartyId = careGroup.Contract.ThirdPartyId
                                    serviceOrderDetail.HealthAdministratorId = careGroup.Contract.HealthAdministratorId
                                End If
                            Case 3 ' Particulares
                                If ThirdPartyPatientId > 0 Then
                                    serviceOrderDetail.ThirdPartyId = ThirdPartyPatientId
                                End If
                        End Select

                        If CType(d, IDictionary(Of String, Object)).ContainsKey("DatasourceType") _
                            AndAlso Integer.Parse(CType(d, IDictionary(Of String, Object))("DatasourceType")) = 12 Then
                            Dim stay = JsonConvert.DeserializeObject(Of CHREGESTA)(d.Stay.ToString())
                            serviceOrderDetail.HospitalStayId = stay.ID
                        End If

                        serviceOrderDetail.ApplyRIAS = Nothing
                        serviceOrderDetail.RIASCupsId = Nothing
                        If CType(d, IDictionary(Of String, Object)).ContainsKey("ApplyRIAS") Then
                            serviceOrderDetail.ApplyRIAS = CBool(d.ApplyRIAS)
                        End If

                        If CType(d, IDictionary(Of String, Object)).ContainsKey("RiasCupsId") AndAlso CType(d, IDictionary(Of String, Object))("RiasCupsId") IsNot Nothing AndAlso CInt(d.RiasCupsId) > 0 _
                            AndAlso serviceOrderDetail.ApplyRIAS Then
                            serviceOrderDetail.RIASCupsId = CInt(d.RiasCupsId)
                        End If

                        serviceOrderDetail.CUPSEntityContractDescriptionId = Nothing
                        If CType(d, IDictionary(Of String, Object)).ContainsKey("CUPSEntityContractDescriptionId") AndAlso CType(d, IDictionary(Of String, Object))("CUPSEntityContractDescriptionId") IsNot Nothing _
                            AndAlso CInt(d.CUPSEntityContractDescriptionId) > 0 Then
                            serviceOrderDetail.CUPSEntityContractDescriptionId = CInt(d.CUPSEntityContractDescriptionId)
                            Dim CupsContractDes = _cupsEntityContractDescriptionsRepository.GetByFilter(Function(x) x.Id = serviceOrderDetail.CUPSEntityContractDescriptionId, False, {"ContractDescriptions"}).FirstOrDefault
                            serviceOrderDetail.ContractDescriptionCodeName = $"{CupsContractDes.ContractDescriptions.Code} - {CupsContractDes.ContractDescriptions.Name}"

                        End If
                    End If

                    serviceOrderDetail.IdTmp = If(serviceOrderDetail.IdTmp > 0, serviceOrderDetail.IdTmp, Identity)

                    If CType(d, IDictionary(Of String, Object)).ContainsKey("IdCita") Then
                        serviceOrderDetail.IdCita = CInt(d.IdCita)
                        serviceOrderDetail.CODSERIPS = d.CupsEntityCode.ToString()
                    End If

                    If CType(d, IDictionary(Of String, Object)).ContainsKey("HemocomponentId") Then
                        serviceOrderDetail.HemocomponentId = CInt(d.HemocomponentId)
                        serviceOrderDetail.CODSERIPS = d.CupsEntityCode.ToString()
                    End If

                    If CType(args, IDictionary(Of String, Object)).ContainsKey("AutorizationNumber") Then
                        serviceOrderDetail.AuthorizationNumber = args.AutorizationNumber.ToString()
                    End If

                    If (CBool(args.IsProcedureQx)) OrElse (Not CBool(args.IsProcedureQx) AndAlso serviceOrderDetail.Presentation = 2) Then
                        'Dim ipsService As IPSService = _
                        Dim costCenter As Domain.Payroll.Entities.CostCenter = _costCenterRepository.GetCostCenterById(serviceOrderDetail.CostCenterId, False)
                        Dim professional As INPROFSAL = _healthProfessionalRepository.GetHealthProfessionalByCode(serviceOrderDetail.PerformsHealthProfessionalCode)
                        With serviceOrderDetail
                            .CodeNameCups = String.Concat(cupsEntity.Code, " - ", cupsEntity.Description)
                            .CodeNameCostCenter = String.Concat(costCenter.Code, " - ", costCenter.Name)
                            If professional IsNot Nothing AndAlso Not String.IsNullOrEmpty(professional.CODPROSAL) Then
                                If professional.ESTADOMED = 1 Then
                                    .CodeNameHealthProfessional = String.Concat(professional.CODPROSAL.Trim(), " - ", professional.NOMMEDICO.Trim())
                                Else
                                    Return New ActionResult(Of ServiceOrder) With {.StateResult = False, .Message = String.Format("El médico {0} se encuentra inactivo en Indigo Vie Cloud Platform", String.Concat(professional.CODPROSAL.Trim(), " - ", professional.NOMMEDICO.Trim()))}
                                End If
                            End If
                            .PerformsHealthProfessionalCode = d.ProfessionalCode.ToString()
                        End With

                        If CType(d, IDictionary(Of String, Object)).ContainsKey("NumeFolio") Then
                            Dim hCQXREALI As HCQXREALI = _hCQXREALIRepository.GetHCQXREALIByAdmissionNumberAndNumeFolioAndCodserips(args.AdmissionNumber.ToString(), d.NumeFolio.ToString(), d.CupsEntityCode.ToString(), If(String.IsNullOrEmpty(d.Auto?.ToString), Nothing, CInt(d.Auto)))
                            If hCQXREALI IsNot Nothing AndAlso hCQXREALI.CONSECUQX > 0 AndAlso Not String.IsNullOrEmpty(hCQXREALI.CODVIAABO) Then
                                If hCQXREALI.HCQXVIABO.ESTADOREG Then
                                    If CType(args.Details, List(Of Object)).ToList().Count > 1 AndAlso hCQXREALI.HCQXVIABO.GENCODVIA IsNot Nothing AndAlso hCQXREALI.HCQXVIABO.GENCODVIA <> 1 Then
                                        serviceOrderDetail.SurgicalInterventionType = hCQXREALI.HCQXVIABO.SurgicalInterventionType
                                    ElseIf CType(args.Details, List(Of Object)).ToList().Count = 1 Then
                                        serviceOrderDetail.SurgicalInterventionType = hCQXREALI.HCQXVIABO.SurgicalInterventionType
                                    End If
                                    serviceOrderDetail.SurgeryNumber = 1 'surgeryNumber
                                    'surgeryNumber += 1
                                End If
                                'serviceOrderDetail.InvoicedQuantity = hCQXREALI.CANTIDAQX
                            End If
                            Dim hCQXEQUIP As List(Of HCQXEQUIP) = _hCQXEQUIPRepository.GetHCQXEQUIPByAdmissionNumberAndNumefolio(args.AdmissionNumber.ToString(), d.NumeFolio.ToString())
                            If hCQXEQUIP IsNot Nothing AndAlso hCQXEQUIP.Any() AndAlso serviceOrderDetail.ServiceOrderDetailSurgical IsNot Nothing AndAlso serviceOrderDetail.ServiceOrderDetailSurgical.Any() Then
                                hCQXEQUIP.ForEach(Sub(o)
                                                      Dim prof As INPROFSAL = _healthProfessionalRepository.GetHealthProfessionalByCode(o.CODPROSAL)
                                                      If prof IsNot Nothing AndAlso Not String.IsNullOrEmpty(prof.CODPROSAL) Then
                                                          Dim third As ThirdParty = _thidPartyRepository.GetThirdPartyByNit(CLng(prof.CODIGONIT.Trim()).ToString())

                                                          If third IsNot Nothing AndAlso third.Id > 0 Then
                                                              Dim surgical As ServiceOrderDetailSurgical = Nothing
                                                              Select Case o.TIPOEQUIP
                                                                  Case "1" 'Cirujano
                                                                      If o.QXPRINCIP Then
                                                                          surgical = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.ClassServiceIps = ResourceManager.GetString("Surgeon", "Contract")).FirstOrDefault()
                                                                      End If
                                                                  Case "2" 'Ayudante
                                                                      surgical = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.ClassServiceIps = ResourceManager.GetString("Assistant", "Contract")).FirstOrDefault()
                                                                  Case "3" 'Anestesiologo
                                                                      surgical = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.ClassServiceIps = ResourceManager.GetString("Anesthesiologist", "Contract")).FirstOrDefault()
                                                                      'Case "4" 'Instrumentadora
                                                                      '    serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.ClassServiceIps = ResourceManager.GetString("Assistant", "Contract")).FirstOrDefault()
                                                                      'Case "5" 'Circulante
                                                                      '    serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.ClassServiceIps = ResourceManager.GetString("Assistant", "Contract")).FirstOrDefault()
                                                                      'Case "6" '6.Otro
                                                                      '    serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.ClassServiceIps = ResourceManager.GetString("Assistant", "Contract")).FirstOrDefault()
                                                              End Select
                                                              If surgical IsNot Nothing Then
                                                                  surgical.PerformsHealthProfessionalCode = prof.CODPROSAL
                                                                  surgical.PerformsHealthProfessionalThirdPartyId = third.Id
                                                              End If
                                                          End If
                                                      End If
                                                  End Sub)
                            End If
                        End If
                    End If

                    If CType(d, IDictionary(Of String, Object)).ContainsKey("TraceabilityPaperworkEventsId") Then
                        serviceOrderDetail.TraceabilityPaperworkEventsId = CInt(d.TraceabilityPaperworkEventsId)
                    End If

                    If CType(d, IDictionary(Of String, Object)).ContainsKey("AuthorizationNumber") Then
                        serviceOrderDetail.AuthorizationNumber = d.AuthorizationNumber.ToString()
                    End If

                    ''se agrega validacion para cuando el numero de la autorizacion no se ha llenado en la orden de servicio este la tome del ingreso creado en el form
                    Dim dictArgs As IDictionary(Of String, Object) = CType(args, IDictionary(Of String, Object))
                    If String.IsNullOrEmpty(serviceOrderDetail.AuthorizationNumber) AndAlso dictArgs.ContainsKey("NewAdmission") AndAlso dictArgs("NewAdmission") IsNot Nothing Then
                        serviceOrderDetail.AuthorizationNumber = dictArgs("NewAdmission").IAUTORIZA?.ToString()?.Trim()
                    End If

                    serviceOrderDetail.CodeNameFunctionalUnit = String.Concat(functionalUnit.Code, " - ", functionalUnit.Name)
                    serviceOrder.ServiceOrderDetail.Add(serviceOrderDetail)
                Next
            Next

            If validation.Length > 0 Then
                Return New ActionResult(Of ServiceOrder) With {.StateResult = False, .Message = validation.ToString()}
            End If
            Return New ActionResult(Of ServiceOrder) With {.StateResult = True, .ObjectEmbbeded = serviceOrder}
        Catch ex As IndigoValidationException
            Return New ActionResult(Of ServiceOrder) With {.StateResult = False, .Message = ex.Message}
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' funcion para crear el Objeto de la orden de servicio y sus detalles para la creacion de la orden de servicio
    ''' </summary>
    ''' <returns></returns>
    Private Function CreateServiceOrderMS(careGroupId As Integer, args As Object) As ActionResult(Of ServiceOrder)
        Try
            Dim CareCenterCode As String = Nothing
            Dim serviceOrder As New ServiceOrder()
            Dim SettingsInventory As New SettingInventory
            If careGroupId = 0 Then
                Throw New Exception
            End If
            Dim careGroup As CareGroup = _careGroupRepository.GetCareGroupById(careGroupId)
            If careGroup Is Nothing OrElse careGroup.Id = 0 Then
                Throw New Exception
            End If
            If CType(args, IDictionary(Of String, Object)).ContainsKey("OperativeUnitId") Then
                Dim OperativeUnitId As Integer = args.OperativeUnitId.ToString().Trim()
                SettingsInventory = _settingInventoryRepository.GetByFilter(Function(x) x.OperatingUnitId = OperativeUnitId, False).FirstOrDefault
            Else
                Throw New Exception
            End If

            With serviceOrder
                .Code = String.Empty
                If CType(args, IDictionary(Of String, Object))("AdmissionNumber") IsNot Nothing Then
                    .AdmissionNumber = args.AdmissionNumber.ToString().Trim()
                End If
                If CType(args, IDictionary(Of String, Object))("CareCenterCode") IsNot Nothing Then
                    CareCenterCode = args.CareCenterCode.ToString().Trim()
                End If
                If CType(args, IDictionary(Of String, Object))("PatientCode") IsNot Nothing Then
                    .PatientCode = args.PatientCode.ToString().Trim()
                End If
                .OrderDate = Date.Now
                If CType(args, IDictionary(Of String, Object))("OperativeUnitId") IsNot Nothing Then
                    .OperatingUnitId = args.OperativeUnitId
                End If
                .EntityName = "AccountControl"
                .EntityCode = ""
                .EntityId = 0
                .Status = 1
                .AffectInventory = False
            End With
            Dim validation As New StringBuilder()

            For Each d As Object In CType(args.Details, List(Of Object)).ToList()
                Dim ServiceOrderDetail = New ServiceOrderDetail()
                With ServiceOrderDetail
                    .CareGroupId = careGroup.Id
                    .HealthAdministratorId = Convert.ToInt16(d.HealthAdministratorId)
                    Select Case careGroup.CareGroupType
                        Case 1 ' EAPB Con contrato
                            If careGroup.ContractId IsNot Nothing Then
                                .ThirdPartyId = careGroup.Contract.ThirdPartyId
                                ServiceOrderDetail.HealthAdministratorId = careGroup.Contract.HealthAdministratorId
                            End If
                    End Select
                    .ServiceType = 0
                    .RecordType = 2
                    .CUPSAssociateService = False
                    .IsPackage = 0
                    .Packaging = 0
                    .LiquidationType = 5
                    .ProductId = Convert.ToInt16(d.ProductId)
                    .InvoicedQuantity = Convert.ToInt16(d.TotalQuantities)
                    .SupplyQuantity = Convert.ToInt16(d.TotalQuantities)
                    .DevolutionQuantity = 0
                    .RateManualSalePrice = Convert.ToDecimal(d.UnitValue)
                    .CostValue = Convert.ToDecimal(d.ProductCost)
                    Dim detailDictionary = TryCast(d, IDictionary(Of String, Object))
                    Dim dispensingDateValue As Object = Nothing
                    If detailDictionary IsNot Nothing AndAlso detailDictionary.TryGetValue("DispensingDate", dispensingDateValue) AndAlso dispensingDateValue IsNot Nothing Then
                        .ServiceDate = Convert.ToDateTime(dispensingDateValue)
                    Else
                        .ServiceDate = DateTime.Now()
                    End If
                    If CType(args, IDictionary(Of String, Object)).ContainsKey("AutorizationNumber") Then
                        .AuthorizationNumber = args.AutorizationNumber.ToString()
                    End If
                    .PerformsFunctionalUnitId = Convert.ToInt16(d.FunctionalUnitId)
                    .PerformsHealthProfessionalCode = Convert.ToString(d.OrderedHealthProfessionalCode)
                    .PerformsProfessionalSpecialty = Convert.ToString(d.PerformsProfessionalSpecialty)
                    .PerformsHealthProfessionalThirdPartyId = Convert.ToInt32(d.OrderedHealthProfessionalThirdPartyId)
                    Dim ProductGroupId = Convert.ToInt16(d.ProductGroupId)
                    Dim ProductGroup = _productGroupsRepository.GetByFilter(Function(x) x.Id = ProductGroupId, False)?.FirstOrDefault
                    If ProductGroup Is Nothing OrElse ProductGroup.Id = 0 Then
                        validation.AppendLine("No se encontró el grupo del producto")
                    End If
                    Select Case SettingsInventory.AssociateCostCenter
                        Case 1
                            Dim Query = _functionalUnitRepository.GetByFilter(Function(x) x.Id = .PerformsFunctionalUnitId, False)?.FirstOrDefault
                            If Query Is Nothing OrElse Query.Id = 0 Then
                                validation.AppendLine("No se encontro Unidad Funcional")
                            End If
                            .CostCenterId = Query.CostCenterId
                        Case 2
                            .CostCenterId = ProductGroup.CostCenterId
                        Case 3
                            .CostCenterId = _warehouseRepository.GetWarehouseById(Convert.ToInt16(d.WarehouseId)).CostCenterId
                    End Select
                    .TaxValue = 0
                    .GrossValue = Convert.ToDecimal(d.UnitValue)
                    .SettlementType = 1
                    .SubTotalSalesPrice = Convert.ToDecimal(d.UnitValue)
                    .ThirdPartyDiscount = 0
                    .ThirdPartyDiscountPercentage = 0
                    .TotalSalesPrice = Convert.ToDecimal(d.UnitValue)
                    .GrandTotalSalesPrice = Convert.ToDecimal(d.TotalValue)
                    .SurchargeApply = CType(d.SurchargeApply, Boolean)
                    .SurgeryNumber = 0
                    .IncomeMainAccountId = ProductGroup.IncomeAccountId
                    .FinalProductCost = CType(d.FinalProductCost, Decimal)
                End With
                serviceOrder.ServiceOrderDetail.Add(ServiceOrderDetail)
            Next
            If validation.Length > 0 Then
                Return New ActionResult(Of ServiceOrder) With {.StateResult = False, .Message = validation.ToString()}
            End If
            Return New ActionResult(Of ServiceOrder) With {.StateResult = True, .ObjectEmbbeded = serviceOrder}
        Catch ex As Exception
            Return New ActionResult(Of ServiceOrder) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function


    ''' <summary>
    ''' Método que genera el detalle de la orden de servicio con los datos de la cotización
    ''' </summary>
    ''' <param name="quotationServiceOrderDetailId"></param>
    ''' <returns></returns>
    Private Function GetServiceOrderDetailByQuotation(quotationServiceOrderDetailId As Integer, serviceDate As Date, functionalUnitId As Integer, professionalSpecialistCode As String, professionalCode As String, thirdPartyMedicoId As Integer) As ServiceOrderDetail
        'Entidad que se arma con los datos de la cotización
        Dim serviceOrderDetail As New ServiceOrderDetail

        'Se obtiene la cotización
        Dim quotationServiceOrderDetail = _cupsEntityRepository.GetQuotationServiceOrderDetailById(quotationServiceOrderDetailId)

        With serviceOrderDetail
            .QuotationServiceOrderDetailId = quotationServiceOrderDetail.Id
            .CareGroupId = quotationServiceOrderDetail.CareGroupId
            .HealthAdministratorId = quotationServiceOrderDetail.HealthAdministratorId
            .ThirdPartyId = quotationServiceOrderDetail.ThirdPartyId
            .ServiceType = quotationServiceOrderDetail.ServiceType
            .RecordType = quotationServiceOrderDetail.RecordType
            .CUPSEntityId = quotationServiceOrderDetail.CUPSEntityId
            .IPSServiceId = quotationServiceOrderDetail.IPSServiceId
            .HospitalStayId = quotationServiceOrderDetail.HospitalStayId
            .HospitalStayDetailId = quotationServiceOrderDetail.HospitalStayDetailId
            .ControlExternalConsultation = quotationServiceOrderDetail.ControlExternalConsultation
            .ControlExternalConsultationCode = quotationServiceOrderDetail.ControlExternalConsultationCode
            .CUPSAssociateService = quotationServiceOrderDetail.CUPSAssociateService
            .CodeAssociateService = quotationServiceOrderDetail.CodeAssociateService
            .IsPackage = quotationServiceOrderDetail.IsPackage
            .Packaging = quotationServiceOrderDetail.Packaging
            .PackageServiceOrderDetailId = quotationServiceOrderDetail.PackageServiceOrderDetailId
            .LiquidationType = quotationServiceOrderDetail.LiquidationType
            .Presentation = quotationServiceOrderDetail.Presentation
            .InvoicedQuantity = quotationServiceOrderDetail.InvoicedQuantity
            .SupplyQuantity = quotationServiceOrderDetail.SupplyQuantity
            .DevolutionQuantity = quotationServiceOrderDetail.DevolutionQuantity
            .RateManualSalePrice = quotationServiceOrderDetail.RateManualSalePrice
            .CostValue = quotationServiceOrderDetail.CostValue
            .ServiceDate = serviceDate
            .AuthorizationNumber = quotationServiceOrderDetail.AuthorizationNumber
            .PerformsFunctionalUnitId = functionalUnitId
            .PerformsHealthProfessionalCode = professionalCode
            .PerformsProfessionalSpecialty = professionalSpecialistCode
            .PerformsHealthProfessionalThirdPartyId = thirdPartyMedicoId
            .BillingConceptId = quotationServiceOrderDetail.BillingConceptId
            .CostCenterId = quotationServiceOrderDetail.CostCenterId
            .SettlementType = quotationServiceOrderDetail.SettlementType
            .IncludeServiceOrderDetailId = quotationServiceOrderDetail.IncludeServiceOrderDetailId
            .RecoveryRatio = quotationServiceOrderDetail.RecoveryRatio
            .RateManualId = quotationServiceOrderDetail.RateManualId
            .RateManualType = quotationServiceOrderDetail.RateManualType
            .RateManualDetailId = quotationServiceOrderDetail.RateManualDetailId
            .DefinitionRateDetailId = quotationServiceOrderDetail.DefinitionRateDetailId
            .DefinitionRateDetailConditionId = quotationServiceOrderDetail.DefinitionRateDetailConditionId
            .SubTotalSalesPrice = quotationServiceOrderDetail.SubTotalSalesPrice
            .ThirdPartyDiscount = quotationServiceOrderDetail.ThirdPartyDiscount
            .ThirdPartyDiscountPercentage = quotationServiceOrderDetail.ThirdPartyDiscountPercentage
            .TotalSalesPrice = quotationServiceOrderDetail.TotalSalesPrice
            .GrandTotalSalesPrice = quotationServiceOrderDetail.GrandTotalSalesPrice
            .SurchargeApply = quotationServiceOrderDetail.SurchargeApply
            .SurgicalInterventionType = quotationServiceOrderDetail.SurgicalInterventionType
            .SurgeryNumber = quotationServiceOrderDetail.SurgeryNumber
            .IsFirstEvent = quotationServiceOrderDetail.IsFirstEvent
            .IsAnnulled = quotationServiceOrderDetail.IsAnnulled
            .IsDelete = quotationServiceOrderDetail.IsDelete
            .IncomeMainAccountId = quotationServiceOrderDetail.IncomeMainAccountId

            .ApplyRIAS = Nothing
            .RIASCupsId = Nothing
            If quotationServiceOrderDetail.ApplyRIAS IsNot Nothing Then
                .ApplyRIAS = quotationServiceOrderDetail.ApplyRIAS
                If quotationServiceOrderDetail.RIASCupsId IsNot Nothing AndAlso quotationServiceOrderDetail.RIASCupsId > 0 Then
                    .RIASCupsId = quotationServiceOrderDetail.RIASCupsId
                End If
            End If

            .CUPSEntityContractDescriptionId = Nothing
            If quotationServiceOrderDetail.CUPSEntityContractDescriptionId IsNot Nothing Then
                .CUPSEntityContractDescriptionId = quotationServiceOrderDetail.CUPSEntityContractDescriptionId
            End If
        End With
        If quotationServiceOrderDetail.QuotationServiceOrderDetailSurgical IsNot Nothing AndAlso quotationServiceOrderDetail.QuotationServiceOrderDetailSurgical.Count > 0 Then
            For Each quotationServiceOrderDetailSurgical In quotationServiceOrderDetail.QuotationServiceOrderDetailSurgical
                Dim serviceOrderDetailSurgical As New ServiceOrderDetailSurgical
                With serviceOrderDetailSurgical
                    .ServiceOrderDetailId = 0
                    .IPSServiceId = quotationServiceOrderDetailSurgical.IPSServiceId
                    .InvoicedQuantity = quotationServiceOrderDetailSurgical.InvoicedQuantity
                    .LiquidationPercentage = quotationServiceOrderDetailSurgical.LiquidationPercentage
                    .RateManualSalePrice = quotationServiceOrderDetailSurgical.RateManualSalePrice
                    .TotalSalesPrice = quotationServiceOrderDetailSurgical.TotalSalesPrice
                    .PerformsHealthProfessionalCode = quotationServiceOrderDetailSurgical.PerformsHealthProfessionalCode
                    .PerformsHealthProfessionalThirdPartyId = quotationServiceOrderDetailSurgical.PerformsHealthProfessionalThirdPartyId
                    .CostValue = quotationServiceOrderDetailSurgical.CostValue
                    .BillingConceptId = quotationServiceOrderDetailSurgical.BillingConceptId
                    .CostCenterId = quotationServiceOrderDetailSurgical.CostCenterId
                    .RateManualDetailSurgicalId = quotationServiceOrderDetailSurgical.RateManualDetailSurgicalId
                    .SurchargeApply = quotationServiceOrderDetailSurgical.SurchargeApply
                    .OnlyMedicalFees = quotationServiceOrderDetailSurgical.OnlyMedicalFees
                    .IncomeMainAccountId = quotationServiceOrderDetailSurgical.IncomeMainAccountId
                End With
                serviceOrderDetail.ServiceOrderDetailSurgical.Add(serviceOrderDetailSurgical)
            Next
        End If

        'Se retorna el detalle de la orden
        Return serviceOrderDetail
    End Function

    ''' <summary>
    ''' Lista los centros de atención
    ''' </summary>
    ''' <param name="UserCode"></param>
    ''' <param name="GroupCode"></param>
    ''' <param name="Container"></param>
    ''' <returns></returns>
    Public Function SP_ListCareCenterHis(UserCode As String, GroupCode As String, Container As String) As ActionResult(Of List(Of SP_ListCareCenterHis_Result)) Implements IAccountControlAdminService.SP_ListCareCenterHis
        Try
            Dim list = _billingControlRepository.SP_ListCareCenterHis(UserCode, GroupCode, Container)
            Return New ActionResult(Of List(Of SP_ListCareCenterHis_Result)) With {.StateResult = True, .ObjectEmbbeded = list}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of SP_ListCareCenterHis_Result)) With {.StateResult = False, .ObjectEmbbeded = Nothing, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Lista las unidades funcionales
    ''' </summary>
    ''' <param name="CareCenterCode"></param>
    ''' <param name="UserCode"></param>
    ''' <param name="GroupCode"></param>
    ''' <param name="Container"></param>
    ''' <returns></returns>
    Public Function SP_ListFunctionalUnitHis(CareCenterCode As String, UserCode As String, GroupCode As String, Container As String) As ActionResult(Of List(Of SP_ListFunctionalUnitHis_Result)) Implements IAccountControlAdminService.SP_ListFunctionalUnitHis
        Try
            Dim list = _billingControlRepository.SP_ListFunctionalUnitHis(CareCenterCode, UserCode, GroupCode, Container)
            Return New ActionResult(Of List(Of SP_ListFunctionalUnitHis_Result)) With {.StateResult = True, .ObjectEmbbeded = list}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of List(Of SP_ListFunctionalUnitHis_Result)) With {.StateResult = False, .ObjectEmbbeded = Nothing, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Metodo para devolver el paramtero de control de autorizacion por tipo de unidad funcional.
    ''' </summary>
    ''' <param name="CareCenterCode"></param>
    ''' <param name="FunctionalUnit"></param>
    ''' <returns></returns>
    Public Function GetAuthorizationParameterByTUF(CareCenterCode As String, FunctionalUnit As String, EmpresaDGH As String) As ActionResult Implements IAccountControlAdminService.GetAuthorizationParameterByTUF
        Try
            Using conexion As New SqlConnection(String.Format(ConfigurationManager.ConnectionStrings(ConfigurationFile.CONX_GENESIS).ConnectionString, "", EmpresaDGH))
                Dim SQLPamateroAutorizacion As String = "USE " & EmpresaDGH & " Select CareGroupId,UnitType,MandatoryAuthorization,[Billing].fnGetUnitType(" & FunctionalUnit & ") As TipoUnidad FROM [Contract].ControlByTypeFunctionalUnit where CareGroupId = " & CareCenterCode
                Dim da As SqlDataAdapter = New SqlDataAdapter(SQLPamateroAutorizacion, conexion)
                da.SelectCommand.CommandTimeout = 90
                    Dim ds As New DataSet
                    da.Fill(ds, "AuthoParameter")
                    Dim INDdtAuthoParameter As DataTable = ds.Tables("AuthoParameter")
                    Dim Result As ActionResult = New ActionResult()
                    If INDdtAuthoParameter IsNot Nothing AndAlso INDdtAuthoParameter.Rows.Count > 0 Then
                        Dim INDdtAuthoParameterRow As DataRow = INDdtAuthoParameter.Select("UnitType = " & INDdtAuthoParameter.Rows(0).Item("TipoUnidad")).FirstOrDefault()
                        If INDdtAuthoParameterRow IsNot Nothing Then
                            Result.StateResult = INDdtAuthoParameterRow.Item("MandatoryAuthorization")
                        Else
                            Result.StateResult = False
                        End If
                    Else
                        Result.StateResult = False
                    End If
                conexion.Close()
                Return Result
            End Using
        Catch ex As Exception
            Dim Result As ActionResult = New ActionResult()
            Result.MessageResult.Add(ex.Message)
            Return Result
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _contractServices.Dispose()
                _billingServices.Dispose()
                _serviceOrderDetailAdminService.Dispose()
                _serviceOrderAdminService.Dispose()
                _billingSequenceAdminService.Dispose()
            End If
            _contractServices = Nothing
            _careGroupRepository = Nothing
            _cupsEntityRepository = Nothing
            _functionalUnitRepository = Nothing
            _billingServices = Nothing
            _serviceOrderDetailAdminService = Nothing
            _patientRepository = Nothing
            _thidPartyRepository = Nothing
            _serviceOrderAdminService = Nothing
            _billingSequenceAdminService = Nothing
            _hCORDLABORepository = Nothing
            _costCenterRepository = Nothing
            _healthProfessionalRepository = Nothing
            _hCORDPROQRepository = Nothing
            _hCQXINFORRepository = Nothing
            _hCORDPRONRepository = Nothing
            _hCPLAOTRPROCUPSRepository = Nothing
            _hCORDPATORepository = Nothing
            _hCORDIMAGRepository = Nothing
            _hCPROCTERRepository = Nothing
            _hCORDINTERepository = Nothing
            _hCHOGASINRepository = Nothing
            _hcCONOXIGRepository = Nothing
            _rateManualRepository = Nothing
            _cupsHomologation = Nothing
            _healthAdministratorRepository = Nothing
            _rateManualDetailRepository = Nothing
            _ipsServiceRepository = Nothing
            _billingConceptRepository = Nothing
            _hCQXREALIRepository = Nothing
            _hCQXEQUIPRepository = Nothing
            _specialityRepository = Nothing
            _hcHISPACARepository = Nothing
            _aMBORDIMARepository = Nothing
            _aMBORDLABRepository = Nothing
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
