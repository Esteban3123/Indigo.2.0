'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Carlos Ernesto Cordoba
' Created          : 24-11-2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting
Imports Domain.Base
Imports Domain.Base.Entities
Imports System.Globalization
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities
Imports Domain.Crystal.Entities
Imports System.Text
Imports Domain.Payroll
Imports Infrastructure.CrossCutting.Base
Imports System.Dynamic
Imports Domain.Crystal

Imports Domain.Entities.Service
Imports System.Xml.Serialization
Imports System.IO
Imports Infrastructure.CrossCutting.Exceptions

#End Region



''' <summary>
''' Encapsula servicios de logica de dominio destinados al modulo de facturación
''' </summary>
Public Class BillingServices
    Implements IBillingServices

#Region "Fields"
    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Billing"
    Private _rateManualValidityRepository As IRateManualValidityRepository
    Private _rateManualRepository As IRateManualRepository
    Private _cupsHomologation As ICupsHomologationRepository
    Private _functionalUnitRepository As IFunctionalUnitRepository
    Private _costCenterRepository As ICostCenterRepository
    Private _cupsRepository As ICupsEntityRepository
    Private _ipsServiceRepository As IIPSServicesRepository
    Private _surgicalProcedureServiceRepository As ISurgicalProcedureServiceRepository
    Private _rateManualDetailSurgicalRepository As IRateManualDetailSurgicalRepository
    Private _rateManualDetailRepository As IRateManualDetailRepository
    Private _revenueControlDetailRepository As IRevenueControlDetailRepository
    Private _productRateDetailRepository As IProductRateDetailRepository
    Private _careGroupRepository As ICareGroupRepository
    Private _inventoryProductRepository As IInventoryProductRepository
    Private _serviceOrderDetailDistributionRepository As IServiceOrderDetailDistributionRepository
    Private _revenueControl As IRevenueControlRepository
    Private _patientRepository As IPatientRepository
    Private _billingAuthorizationRepository As IBillingAuthorizationRepository
    Private _settingsBillingRepository As ISettingsBillingRepository
    Private _serviceOrderDetailRepository As IServiceOrderDetailRepository
    Private _mainAccountsRepository As IPUCRepository
    Private _settingsInventoryRepository As ISettingInventoryRepository
    Private _customerRepository As ICustomerRepository
    Private _contractServices As IContractServices
    Private _productGroupRepository As IProductGroupsRepository
    Private _iNPACIENTTOPANURepository As IINPACIENTTOPANURepository
    Private _careGroupDefinitionRateRepository As ICareGroupDefinitionRateRepository
    Private _definitionRateDetailConditionRepository As IDefinitionRateDetailConditionRepository
    Private _definitionRateDetailRepository As IDefinitionRateDetailRepository
    Private _billingConceptRepository As IIPSServiceGroupRepository
    Private _invoiceRepository As IInvoiceRepository
    Private _contractExternalClientsRepository As IContractExternalClientsRepository
    Private _cupsHomologationRepository As ICupsHomologationRepository
    Private _companySettingsRepository As ICompanySettingsRepository

    Private Const UVRNUMBER As Integer = 450

    ''' <summary>
    ''' Diccionario para reutilizar los grupos a la hora de consultar
    ''' </summary>
    Private _dictionaryProductGroups As New Dictionary(Of Integer, ProductGroup)
    Private _dictionaryCupsEntity As New Dictionary(Of Integer, CUPSEntity)
    Private _dictionaryIpsService As New Dictionary(Of Integer, IPSService)
    Private _dictionaryCostCenter As New Dictionary(Of Integer, Domain.Payroll.Entities.CostCenter)
    Private _dictionaryFunctionalUnit As New Dictionary(Of Integer, Domain.Payroll.Entities.FunctionalUnit)
    Private _dictionaryCareGroup As Dictionary(Of Integer, CareGroup)
    Private _dictionaryMainAccounts As Dictionary(Of Integer, MainAccounts)
    Private _dictionaryBillingConcept As New Dictionary(Of Integer, BillingConcept)
    Private _dictionaryContractExternalClient As Dictionary(Of Integer, ContractExternalClients)
    Private _dictionaryDefinitionRate As Dictionary(Of Tuple(Of Integer, Date), ContractExternalClientsDefinitionRate)
#End Region

#Region "Builders"
    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(rateManualValidityRepository As IRateManualValidityRepository,
                   rateManualRepository As IRateManualRepository,
                   cupsHomologation As ICupsHomologationRepository,
                   functionalUnitRepository As IFunctionalUnitRepository,
                   costCenterRepository As ICostCenterRepository,
                   cupsRepository As ICupsEntityRepository,
                   ipsServiceRepository As IIPSServicesRepository,
                   surgicalProcedureServiceRepository As ISurgicalProcedureServiceRepository,
                   rateManualDetailSurgicalRepository As IRateManualDetailSurgicalRepository,
                   rateManualDetailRepository As IRateManualDetailRepository,
                   revenueControlDetailRepository As IRevenueControlDetailRepository,
                   productRateDetailRepository As IProductRateDetailRepository,
                   careGroupRepository As ICareGroupRepository,
                   inventoryProductRepository As IInventoryProductRepository,
                   revenueControl As IRevenueControlRepository,
                   patientRepository As IPatientRepository,
                   billingAuthorizationRepository As IBillingAuthorizationRepository,
                   settingsBillingRepository As ISettingsBillingRepository,
                   serviceOrderDetailRepository As IServiceOrderDetailRepository, serviceOrderDetailDistributionRepository As IServiceOrderDetailDistributionRepository,
                   mainAccountsRepository As IPUCRepository, settingsInventoryRepository As ISettingInventoryRepository, customerRepository As ICustomerRepository,
                   contractServices As IContractServices, productGroupRepository As IProductGroupsRepository, iNPACIENTTOPANURepository As IINPACIENTTOPANURepository,
                   careGroupDefinitionRateRepository As ICareGroupDefinitionRateRepository, definitionRateDetailConditionRepository As IDefinitionRateDetailConditionRepository,
                   definitionRateDetailRepository As IDefinitionRateDetailRepository, billingConceptRepository As IIPSServiceGroupRepository, invoiceRepository As IInvoiceRepository,
                   ContractExternalClientsRepository As IContractExternalClientsRepository, CupsHomologationRepository As ICupsHomologationRepository,
                   CompanySettingsRepository As ICompanySettingsRepository)
        Me._revenueControlDetailRepository = revenueControlDetailRepository
        Me._rateManualValidityRepository = rateManualValidityRepository
        Me._rateManualRepository = rateManualRepository
        Me._cupsHomologation = cupsHomologation
        Me._functionalUnitRepository = functionalUnitRepository
        Me._costCenterRepository = costCenterRepository
        Me._cupsRepository = cupsRepository
        Me._ipsServiceRepository = ipsServiceRepository
        Me._surgicalProcedureServiceRepository = surgicalProcedureServiceRepository
        Me._rateManualDetailSurgicalRepository = rateManualDetailSurgicalRepository
        Me._rateManualDetailRepository = rateManualDetailRepository
        Me._productRateDetailRepository = productRateDetailRepository
        Me._careGroupRepository = careGroupRepository
        Me._inventoryProductRepository = inventoryProductRepository
        Me._serviceOrderDetailDistributionRepository = serviceOrderDetailDistributionRepository
        Me._revenueControl = revenueControl
        Me._patientRepository = patientRepository
        Me._billingAuthorizationRepository = billingAuthorizationRepository
        Me._settingsBillingRepository = settingsBillingRepository
        Me._serviceOrderDetailRepository = serviceOrderDetailRepository
        Me._mainAccountsRepository = mainAccountsRepository
        Me._settingsInventoryRepository = settingsInventoryRepository
        Me._customerRepository = customerRepository
        Me._contractServices = contractServices
        Me._productGroupRepository = productGroupRepository
        Me._iNPACIENTTOPANURepository = iNPACIENTTOPANURepository
        Me._billingConceptRepository = billingConceptRepository
        Me._invoiceRepository = invoiceRepository
        Me._contractExternalClientsRepository = ContractExternalClientsRepository
        Me._cupsHomologationRepository = CupsHomologationRepository
        Me._companySettingsRepository = CompanySettingsRepository
        _careGroupDefinitionRateRepository = careGroupDefinitionRateRepository
        _definitionRateDetailConditionRepository = definitionRateDetailConditionRepository
        _definitionRateDetailRepository = definitionRateDetailRepository
        _dictionaryProductGroups = New Dictionary(Of Integer, ProductGroup)()
        _dictionaryCupsEntity = New Dictionary(Of Integer, CUPSEntity)()
        _dictionaryFunctionalUnit = New Dictionary(Of Integer, Domain.Payroll.Entities.FunctionalUnit)()
        _dictionaryCostCenter = New Dictionary(Of Integer, Domain.Payroll.Entities.CostCenter)()
        _dictionaryIpsService = New Dictionary(Of Integer, IPSService)()
        _dictionaryCareGroup = New Dictionary(Of Integer, CareGroup)()
        _dictionaryMainAccounts = New Dictionary(Of Integer, MainAccounts)()
        _dictionaryBillingConcept = New Dictionary(Of Integer, BillingConcept)()
        _dictionaryContractExternalClient = New Dictionary(Of Integer, ContractExternalClients)
    End Sub

#End Region

#Region "Methods"


#Region "GetDictionary"
    Private Function GetMainAccounts(id As Integer) As MainAccounts
        Dim mainAccounts As MainAccounts
        If _dictionaryMainAccounts.ContainsKey(id) Then
            mainAccounts = _dictionaryMainAccounts(id)
        Else
            mainAccounts = _mainAccountsRepository.GetAccountByIdIncludes(id, Nothing)
            _dictionaryMainAccounts.Add(id, mainAccounts)
        End If
        Return mainAccounts
    End Function

    Public Function GetCupsEntity(id As Integer, tracking As Boolean) As CUPSEntity
        If _dictionaryCupsEntity Is Nothing Then
            _dictionaryCupsEntity = New Dictionary(Of Integer, CUPSEntity)()
        End If
        Dim cups As CUPSEntity = Nothing
        If _dictionaryCupsEntity.ContainsKey(id) Then
            cups = _dictionaryCupsEntity(id)
        Else
            cups = _cupsRepository.GetCupsEntityById(id, tracking)
            If cups IsNot Nothing AndAlso cups.Id > 0 Then
                _dictionaryCupsEntity.Add(cups.Id, cups)
            End If
        End If
        Return cups
    End Function

    Public Function GetCareGroup(id As Integer) As CareGroup
        Dim CareGroup As CareGroup = Nothing
        If _dictionaryCareGroup.ContainsKey(id) Then
            CareGroup = _dictionaryCareGroup(id)
        Else
            CareGroup = _careGroupRepository.GetCareGroupById(id)
            _dictionaryCareGroup.Add(CareGroup.Id, CareGroup)
        End If
        Return CareGroup
    End Function

    Public Function GetContractExternalClient(id As Integer) As ContractExternalClients
        Dim ContractExternalClient As ContractExternalClients = Nothing
        If _dictionaryContractExternalClient.ContainsKey(id) Then
            ContractExternalClient = _dictionaryContractExternalClient(id)
        Else
            ContractExternalClient = _contractExternalClientsRepository.GetContractExternalClientsById(id)
            _dictionaryContractExternalClient.Add(ContractExternalClient.Id, ContractExternalClient)
        End If
        Return ContractExternalClient
    End Function

    Public Function GetBillingConcept(id As Integer) As BillingConcept
        Dim billingConcept As BillingConcept = Nothing
        If _dictionaryBillingConcept.ContainsKey(id) Then
            billingConcept = _dictionaryBillingConcept(id)
        Else
            billingConcept = _billingConceptRepository.GetBillingConceptByIdIncludes(id, {"BillingConceptAccount"}.ToArray())
            _dictionaryBillingConcept.Add(id, billingConcept)
        End If
        Return billingConcept
    End Function

    Public Function GetContractExternalCDefinitionRate(ContractExternalClientsId As Integer, serviceDate As Date) As ContractExternalClientsDefinitionRate
        If _dictionaryDefinitionRate Is Nothing Then
            _dictionaryDefinitionRate = New Dictionary(Of Tuple(Of Integer, Date), ContractExternalClientsDefinitionRate)()
        End If
        Dim drd As ContractExternalClientsDefinitionRate = Nothing
        If _dictionaryDefinitionRate.ContainsKey(New Tuple(Of Integer, Date)(ContractExternalClientsId, serviceDate)) Then
            drd = _dictionaryDefinitionRate(New Tuple(Of Integer, Date)(ContractExternalClientsId, serviceDate))
        Else
            drd = _contractExternalClientsRepository.GetContractExternalClientsDefinitionRateByContractEIdServiceDate(ContractExternalClientsId, serviceDate, False)
            If drd IsNot Nothing AndAlso drd.Id > 0 Then
                _dictionaryDefinitionRate.Add(New Tuple(Of Integer, Date)(ContractExternalClientsId, serviceDate), drd)
            End If
        End If
        Return drd
    End Function

    Public Function GetIpsService(id As Integer) As IPSService
        If _dictionaryIpsService Is Nothing Then
            _dictionaryIpsService = New Dictionary(Of Integer, IPSService)()
        End If
        Dim ipsService As IPSService = Nothing
        If _dictionaryIpsService.ContainsKey(id) Then
            ipsService = _dictionaryIpsService(id)
        Else
            ipsService = _ipsServiceRepository.GetIPSServiceById(id, False)
            If ipsService IsNot Nothing AndAlso ipsService.Id > 0 Then
                _dictionaryIpsService.Add(ipsService.Id, ipsService)
            End If
        End If
        Return ipsService
    End Function
#End Region

    ''' <summary>
    ''' Gets the service value.
    ''' </summary>
    ''' <param name="careGroupId">The care group identifier.</param>
    ''' <param name="ProductId">The product identifier.</param>
    ''' <returns></returns>
    Function GetProductRateDetail(careGroupId As Integer, ProductId As Integer, serviceDate As Date) As ActionResult(Of ProductRateDetail) Implements IBillingServices.GetProductRateDetail
        Dim productRateDetail As ProductRateDetail = _productRateDetailRepository.GetProductRateDetailByCareGroupIdProductIdServiceDate(careGroupId, ProductId, serviceDate.Date)
        If productRateDetail IsNot Nothing AndAlso productRateDetail.Id > 0 Then
            Return New ActionResult(Of ProductRateDetail) With {.StateResult = True, .ObjectEmbbeded = productRateDetail}
        Else
            Dim careGroup As CareGroup = _careGroupRepository.GetCareGroupById(careGroupId)
            Dim product As InventoryProduct = _inventoryProductRepository.GetInventoryProductById(ProductId)
            Return New ActionResult(Of ProductRateDetail) With {.StateResult = False, .Message = String.Format(ResourceManager.GetString("ProductNoTarifaWithName", "Inventory"), String.Concat(product.Code, " - ", product.Name), String.Concat(careGroup.Code, " - ", careGroup.Name), serviceDate.Date)}
        End If
    End Function

    Private Function ValidationsContract(contract As Contract) As ActionResult Implements IBillingServices.ValidationsContract
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

    ''' <summary>
    ''' Gets the service value.
    ''' </summary>
    ''' <param name="CupsEntityId">Id del CUPS</param>
    ''' <param name="IPSServiceId">Id del servicio IPS</param>
    ''' <param name="CareGroupId">Id del grupo de atencion</param>
    ''' <param name="FunctionalUnitId">Id de la unidad funcional</param>
    ''' <param name="Specialty">Especialidad del medico</param>
    ''' <param name="ServiceDate">Fecha del servicio</param>
    ''' <param name="PatientGenus">Genero del paciente</param>
    ''' <param name="PatientDateBirth">Fecha de nacimiento del paciente</param>
    ''' <param name="InvoicedQuantity">Cantidad del servicio</param>
    ''' <param name="ProfessionalHealthCode">Codigo del medico</param>
    ''' <param name="ProfessionalHealthThirdPartyId">Id del tercero asignado al medico</param>
    ''' <returns></returns>
    Public Function GetServiceValue(AdmissionNumber As String, CenterAttentionCode As String, CupsEntityId As Integer, IPSServiceId As Integer, CareGroupId As Integer, FunctionalUnitId As Integer?, Specialty As String, ServiceDate As Date, PatientGenus As Integer, PatientDateBirth As Date, InvoicedQuantity As Integer, ProfessionalHealthCode As String, ProfessionalHealthThirdPartyId As Integer?, Optional RiasId As Integer? = Nothing, Optional ContractDescriptionId As Integer? = Nothing) As ActionResult(Of ServiceOrderDetail) Implements IBillingServices.GetServiceValue
        Try
            'Se consume el procedimiento almacenado
            Dim resultStore = Me._serviceOrderDetailRepository.SP_GetServiceValue(AdmissionNumber, CenterAttentionCode, CupsEntityId, IPSServiceId, CareGroupId, FunctionalUnitId, Specialty, ServiceDate, PatientGenus, PatientDateBirth, InvoicedQuantity, ProfessionalHealthCode, ProfessionalHealthThirdPartyId, RiasId, ContractDescriptionId)

            If resultStore Is Nothing Then
                Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = False, .Message = "No se generó el detalle de la orden de servicio"}
            ElseIf resultStore.StatusResult = False Then
                Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = False, .Message = resultStore.MessageResult}
            End If

            Dim serviceOrderDetail As New ServiceOrderDetail
            serviceOrderDetail.StartTracking()

            With serviceOrderDetail
                .CareGroupId = resultStore.CareGroupId
                .ServiceType = resultStore.ServiceType
                .RecordType = resultStore.RecordType
                .CUPSEntityId = resultStore.CUPSEntityId
                .CodeNameCups = resultStore.CodeNameCups
                .IPSServiceId = resultStore.IPSServiceId
                .CodeNameIpsService = resultStore.CodeNameIpsService
                .LiquidationType = resultStore.LiquidationType
                .Presentation = resultStore.Presentation
                .InvoicedQuantity = resultStore.InvoicedQuantity
                .RateManualSalePrice = resultStore.RateManualSalePrice
                .CostValue = resultStore.CostValue
                .ServiceDate = resultStore.ServiceDate
                .PerformsFunctionalUnitId = resultStore.PerformsFunctionalUnitId
                .CodeNameFunctionalUnit = resultStore.CodeNameFunctionalUnit
                .PerformsHealthProfessionalCode = resultStore.PerformsHealthProfessionalCode
                .PerformsProfessionalSpecialty = resultStore.PerformsProfessionalSpecialty
                .PerformsHealthProfessionalThirdPartyId = resultStore.PerformsHealthProfessionalThirdPartyId
                .BillingConceptId = resultStore.BillingConceptId
                .CostCenterId = resultStore.CostCenterId
                .CodeNameCostCenter = resultStore.CodeNameCostCenter
                .SettlementType = resultStore.SettlementType
                .RateManualId = resultStore.RateManualId
                .RateManualType = resultStore.RateManualType
                .RateManualDetailId = resultStore.RateManualDetailId
                .DefinitionRateDetailId = resultStore.DefinitionRateDetailId
                .DefinitionRateDetailConditionId = resultStore.DefinitionRateDetailConditionId
                .SurchargeApply = resultStore.SurchargeApply
                .SurgicalInterventionType = resultStore.SurgicalInterventionType
                .LiquidateAllMIVIE = resultStore.LiquidateAllMIVIE
                .TaxedService = resultStore.TaxedService
                .TaxPercent = resultStore.TaxPercent
                .SubTotalSalesPrice = resultStore.SubTotalSalesPrice
                .GrossValue = resultStore.SubTotalSalesPrice

                Dim _dictionaryValues = Utils.SetValueSalesPrice(resultStore.SalePriceIncludeTax, resultStore.RateManualSalePrice, .TaxPercent)
                If _dictionaryValues?.Any() Then
                    .SubTotalSalesPrice = _dictionaryValues?.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.SubtotalSalesPrice).Value
                    .GrossValue = _dictionaryValues?.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.GrossValue).Value
                    .TaxValue = _dictionaryValues?.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.TaxValue).Value
                End If

                .TotalSalesPrice = .SubTotalSalesPrice
                .GrandTotalSalesPrice = .TotalSalesPrice * resultStore.InvoicedQuantity

                If resultStore.IncomeMainAccountId Is Nothing Then
                    Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = False, .Message = "El concepto asociado al cups no tiene cuenta contable de ingreso parametrizada"}
                Else
                    .IncomeMainAccountId = resultStore.IncomeMainAccountId
                End If
                .RoundService = resultStore.RoundService
                .IsSOAT = resultStore.IsSOAT
                .AllowValueChange = resultStore.AllowValueChange
            End With

            If Not String.IsNullOrEmpty(resultStore.ServiceOrderDetailSurgicalXml) Then
                Dim ServiceOrderDetailSurgicalXml As String = "<ListServiceOrderDetailSurgical>" & resultStore.ServiceOrderDetailSurgicalXml & "</ListServiceOrderDetailSurgical>"

                Dim serializer As New XmlSerializer(GetType(ListServiceOrderDetailSurgicalXml))
                Using reader As TextReader = New StringReader(ServiceOrderDetailSurgicalXml)
                    Dim listServiceOrderDetailSurgicalXml = serializer.Deserialize(reader)
                    For Each sods As ServiceOrderDetailSurgicalXml In listServiceOrderDetailSurgicalXml.ServiceOrderDetailSurgicalXml
                        Dim serviceOrderDetailSurgical As New ServiceOrderDetailSurgical
                        serviceOrderDetailSurgical.StartTracking()
                        With serviceOrderDetailSurgical
                            .CodeNameIpsService = sods.CodeNameIpsService
                            .IPSServiceId = sods.IPSServiceId
                            .InvoicedQuantity = sods.InvoicedQuantity
                            .LiquidationPercentage = sods.LiquidationPercentage
                            .TotalSalesPrice = sods.TotalSalesPrice
                            .ClassServiceIps = sods.ClassServiceIps
                            .RateManualSalePrice = sods.RateManualSalePrice
                            .PerformsHealthProfessionalCode = sods.PerformsHealthProfessionalCode
                            .PerformsHealthProfessionalThirdPartyId = sods.PerformsHealthProfessionalThirdPartyId
                            .CostValue = sods.CostValue
                            .BillingConceptId = sods.BillingConceptId
                            .CostCenterId = sods.CostCenterId
                            .RateManualDetailSurgicalId = sods.RateManualDetailSurgicalId
                            .SurchargeApply = sods.SurchargeApply
                            .RoundService = sods.RoundService
                            .AllowValueChange = sods.AllowValueChange
                        End With
                        serviceOrderDetail.ServiceOrderDetailSurgical.Add(serviceOrderDetailSurgical)
                    Next
                    Dim _sumTotalSalePrice = serviceOrderDetail?.ServiceOrderDetailSurgical?.Sum(Function(x) x.TotalSalesPrice)
                    Dim _dictionaryValues = Utils.SetValueSalesPrice(resultStore.SalePriceIncludeTax,
                                                                  _sumTotalSalePrice,
                                                                  resultStore.TaxPercent)

                    If _dictionaryValues?.Any() Then
                        serviceOrderDetail.SubTotalSalesPrice = _dictionaryValues?.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.SubtotalSalesPrice).Value
                        serviceOrderDetail.GrossValue = _dictionaryValues?.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.GrossValue).Value
                        serviceOrderDetail.TaxValue = _dictionaryValues?.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.TaxValue).Value
                        serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.SubTotalSalesPrice
                        serviceOrderDetail.GrandTotalSalesPrice = serviceOrderDetail.TotalSalesPrice * resultStore.InvoicedQuantity
                    End If
                End Using
            End If

            'Se devuelve el mensaje
            Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = True, .ObjectEmbbeded = serviceOrderDetail}
        Catch ex As Exception
            Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = False, .Message = ex.ToString}
        End Try
    End Function

    ''' <summary>
    ''' obtiene el valor con recargo
    ''' </summary>
    ''' <param name="serviceOrderDetail"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetServiceValueSurcharge(serviceOrderDetail As ServiceOrderDetail) As ServiceOrderDetail Implements IBillingServices.GetServiceValueSurcharge
        'consulto las tarifas y el retorna en un agregado la tarifa correspondiente
        Dim rateVariation As Decimal = 0
        Dim salesValue As Decimal = 0 'Solo se llena si la tarifa es Fija
        Dim salesValueWithSurcharge As Decimal = 0 'Solo se llena si la tarifa es Fija
        Dim dictionarySalesValue As Dictionary(Of Utils.eTypeTaxControl, Decimal)
        Dim SalePriceIncludeTax = _companySettingsRepository.FirstOrDefault(Function(x) x.Id > 0).SalePriceIncludeTax 'se consulta el parametro de empresa si es impuesto incluido o no

        If serviceOrderDetail.DefinitionRateDetailConditionId IsNot Nothing Then
            Dim condition = _definitionRateDetailConditionRepository.GetDefinitionRateDetailConditionById(serviceOrderDetail.DefinitionRateDetailConditionId, False)
            rateVariation = If(condition.RateVariation, 0)
            If condition.LiquidationType = 1 Then 'Si es fija
                salesValue = condition.SalesValue
                salesValueWithSurcharge = condition.SalesValueWithSurcharge
            End If
        Else 'Saco la tarifa de la cabecera
            Dim detail = _definitionRateDetailRepository.GetDefinitionRateDetailById(serviceOrderDetail.DefinitionRateDetailId, False)
            rateVariation = If(detail.RateVariation, 0)
            If detail.LiquidationType = 1 Then 'Si es fija
                salesValue = detail.SalesValue
                salesValueWithSurcharge = detail.SalesValueWithSurcharge
            End If
        End If
        If serviceOrderDetail.Presentation = 2 Then
            ' si es quirurgico recorro los detalles quirurgico para cambiar los valores 
            For Each item In serviceOrderDetail.ServiceOrderDetailSurgical
                item.RoundService = 1
                If item.RateManualDetailSurgicalId IsNot Nothing Then
                    Dim rateManualDetalSurgical = _rateManualDetailSurgicalRepository.GetRateManualDetailSurgicalById(item.RateManualDetailSurgicalId)
                    Dim costValueItem As Decimal
                    If serviceOrderDetail.SurchargeApply = True Then
                        costValueItem = rateManualDetalSurgical.SalesValueWithSurcharge * item.InvoicedQuantity
                    Else
                        costValueItem = rateManualDetalSurgical.SalesValue * item.InvoicedQuantity
                    End If
                    If item.ClassServiceIps?.ToUpper() = ResourceManager.GetString("RightRoom", "Contract")?.ToUpper() Then
                        If serviceOrderDetail.SurgicalInterventionType = 9 Then
                            Dim rateManual = _rateManualRepository.GetRateManualById(serviceOrderDetail.RateManualId)
                            costValueItem = costValueItem * rateManual.PercentageNoBloodyRoom / 100
                        End If
                    End If

                    Dim totalSalesPriceItem = costValueItem + (costValueItem * (rateVariation / 100))
                    totalSalesPriceItem = Utils.RoundValue(totalSalesPriceItem, rateManualDetalSurgical.RateManual.RoundService)
                    item.RateManualSalePrice = totalSalesPriceItem
                    item.TotalSalesPrice = totalSalesPriceItem
                    item.RoundService = rateManualDetalSurgical.RateManual.RoundService
                End If
                item.SurchargeApply = True
            Next
            dictionarySalesValue = Utils.SetValueSalesPrice(SalePriceIncludeTax, serviceOrderDetail.ServiceOrderDetailSurgical.Sum(Function(x) x.TotalSalesPrice), serviceOrderDetail.TaxPercent)
            serviceOrderDetail.SubTotalSalesPrice = dictionarySalesValue.FirstOrDefault(Function(s) s.Key = Utils.eTypeTaxControl.SubtotalSalesPrice).Value 'serviceOrderDetail.ServiceOrderDetailSurgical.Sum(Function(x) x.TotalSalesPrice)
            serviceOrderDetail.RateManualSalePrice = serviceOrderDetail.ServiceOrderDetailSurgical.Sum(Function(x) x.TotalSalesPrice)
            serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.SubTotalSalesPrice
            serviceOrderDetail.GrossValue = dictionarySalesValue.FirstOrDefault(Function(s) s.Key = Utils.eTypeTaxControl.GrossValue).Value
            serviceOrderDetail.TaxValue = dictionarySalesValue.FirstOrDefault(Function(s) s.Key = Utils.eTypeTaxControl.TaxValue).Value
            serviceOrderDetail.GrandTotalSalesPrice = serviceOrderDetail.TotalSalesPrice * serviceOrderDetail.InvoicedQuantity
            serviceOrderDetail.RoundService = 0
        ElseIf serviceOrderDetail.RateManualDetailId IsNot Nothing Then
            'consulto el manual de tarifas directamente por el id y obtengo el valor con recargo
            Dim rateManualDetail = _rateManualDetailRepository.GetRateManualDetailById(serviceOrderDetail.RateManualDetailId)
            Dim totalSalesPriceItem As Decimal
            If serviceOrderDetail.SurchargeApply = True Then
                totalSalesPriceItem = rateManualDetail.SalesValueWithSurcharge + (rateManualDetail.SalesValueWithSurcharge * (rateVariation / 100))
            Else
                totalSalesPriceItem = rateManualDetail.SalesValue + (rateManualDetail.SalesValue * (rateVariation / 100))
            End If
            totalSalesPriceItem = Utils.RoundValue(totalSalesPriceItem, rateManualDetail.RateManual.RoundService)

            dictionarySalesValue = Utils.SetValueSalesPrice(SalePriceIncludeTax, totalSalesPriceItem, serviceOrderDetail.TaxPercent)
            serviceOrderDetail.RateManualSalePrice = totalSalesPriceItem
            serviceOrderDetail.SubTotalSalesPrice = dictionarySalesValue.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.SubtotalSalesPrice).Value
            serviceOrderDetail.GrossValue = dictionarySalesValue.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.GrossValue).Value
            serviceOrderDetail.TaxValue = dictionarySalesValue.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.TaxValue).Value
            serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.SubTotalSalesPrice
            serviceOrderDetail.GrandTotalSalesPrice = serviceOrderDetail.TotalSalesPrice * serviceOrderDetail.InvoicedQuantity
            serviceOrderDetail.RoundService = 0
        Else
            dictionarySalesValue = Utils.SetValueSalesPrice(SalePriceIncludeTax, If(serviceOrderDetail.SurchargeApply, salesValueWithSurcharge, salesValue), serviceOrderDetail.TaxPercent)
            serviceOrderDetail.RateManualSalePrice = If(serviceOrderDetail.SurchargeApply, salesValueWithSurcharge, salesValue)
            serviceOrderDetail.CostValue = dictionarySalesValue.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.SubtotalSalesPrice).Value
            serviceOrderDetail.SubTotalSalesPrice = serviceOrderDetail.CostValue
            serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.SubTotalSalesPrice
            serviceOrderDetail.GrossValue = dictionarySalesValue.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.GrossValue).Value
            serviceOrderDetail.TaxValue = dictionarySalesValue.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.TaxValue).Value
            serviceOrderDetail.GrandTotalSalesPrice = serviceOrderDetail.TotalSalesPrice * serviceOrderDetail.InvoicedQuantity
            serviceOrderDetail.RoundService = 0
        End If
        Return serviceOrderDetail
    End Function

    ''' <summary>
    ''' obtener el valor de los detalles del ips quirurgico cuando el usuario cambia los valores por defecto en el formulario
    ''' </summary>
    ''' <param name="serviceOrderDetail"></param>
    ''' <param name="listSurgicalProcedureServiceDefault"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetServiceValueBySurgicalProcedureService(serviceOrderDetail As ServiceOrderDetail, listSurgicalProcedureServiceDefault As List(Of SurgicalProcedureService)) As ActionResult(Of ServiceOrderDetail) Implements IBillingServices.GetServiceValueBySurgicalProcedureService
        Return _contractServices.GetServiceValueBySurgicalProcedureService(serviceOrderDetail, listSurgicalProcedureServiceDefault)
    End Function

    ''' <summary>
    ''' metodo para recalcular los eventos cuando se cambie el item que es primer evento
    ''' </summary>
    ''' <param name="serviceOrderDetail"></param>
    ''' <returns></returns>
    Public Function RecalculateSurgicalEvents(serviceOrderDetail As ServiceOrderDetail) As ServiceOrderDetail Implements IBillingServices.RecalculateSurgicalEvents
        'consulto las tarifas y el retorna en un agregado la tarifa correspondiente
        Dim rateVariation As Decimal = 0
        Dim rateManual As RateManual = Nothing
        Dim SalePriceIncludeTax = _companySettingsRepository.FirstOrDefault(Function(x) x.Id > 0).SalePriceIncludeTax 'se consulta el parametro de empresa si es impuesto incluido o no
        Dim result = _contractServices.GetRateValue(serviceOrderDetail.IPSServiceId, serviceOrderDetail.CUPSEntityId, serviceOrderDetail.CareGroupId,
                                                    serviceOrderDetail.PerformsFunctionalUnitId, serviceOrderDetail.PerformsProfessionalSpecialty,
                                                    serviceOrderDetail.ServiceDate, ERateOptions.IPSSERVICE)
        If result.ObjectEmbbededAux IsNot Nothing Then 'Fue por que la tarifa se saco de una condicion
            'asigno la bandera para saber si en presentacion puedo cambiar el valor del servicio o no
            serviceOrderDetail.AllowValueChange = result.ObjectEmbbeded.AllowValueChange
            serviceOrderDetail.LiquidationType = result.ObjectEmbbededAux.LiquidationType
            If result.ObjectEmbbededAux.LiquidationType = 2 Then 'Si es estandar
                rateVariation = result.ObjectEmbbededAux.RateVariation
                rateManual = _rateManualRepository.GetRateManualById(result.ObjectEmbbededAux.RateManualId)
            ElseIf result.ObjectEmbbededAux.LiquidationType = 3 Then 'Si es por vigencia
                rateVariation = result.ObjectEmbbededAux.RateVariation
                Dim rateManualValidity = _rateManualValidityRepository.GetRateManualValidityByIdWithAggregates(result.ObjectEmbbededAux.RateManualValidityId)
                Dim rateManualValidityDetail = rateManualValidity.RateManualValidityDetail.Where(Function(d) d.InitialDate <= serviceOrderDetail.ServiceDate AndAlso d.EndDate >= serviceOrderDetail.ServiceDate).FirstOrDefault
                If rateManualValidityDetail IsNot Nothing Then
                    rateManual = _rateManualRepository.GetRateManualById(rateManualValidityDetail.RateManualId)
                End If
            End If
        Else 'Es por que la condicion fue ninguna y la tarifa esta en la cabecera
            'asigno la bandera para saber si en presentacion puedo cambiar el valor del servicio o no
            serviceOrderDetail.AllowValueChange = result.ObjectEmbbeded.AllowValueChange
            serviceOrderDetail.LiquidationType = result.ObjectEmbbeded.LiquidationType
            If result.ObjectEmbbeded.LiquidationType = 2 Then 'Si es estandar
                rateVariation = result.ObjectEmbbeded.RateVariation
                rateManual = _rateManualRepository.GetRateManualById(result.ObjectEmbbeded.RateManualId)
            ElseIf result.ObjectEmbbeded.LiquidationType = 3 Then 'Si es por vigencia
                rateVariation = result.ObjectEmbbeded.RateVariation
                Dim rateManualValidity = _rateManualValidityRepository.GetRateManualValidityByIdWithAggregates(result.ObjectEmbbeded.RateManualValidityId)
                Dim rateManualValidityDetail = rateManualValidity.RateManualValidityDetail.Where(Function(d) d.InitialDate <= serviceOrderDetail.ServiceDate AndAlso d.EndDate >= serviceOrderDetail.ServiceDate).FirstOrDefault
                If rateManualValidityDetail IsNot Nothing Then
                    rateManual = _rateManualRepository.GetRateManualById(rateManualValidityDetail.RateManualId)
                End If
            Else
                rateManual = _rateManualRepository.GetRateManualById(result.ObjectEmbbeded.RateManualId)
            End If
        End If
        Dim IPSService = _ipsServiceRepository.GetIPSServiceById(serviceOrderDetail.IPSServiceId)
        Dim costValueItem As Decimal
        For Each item In serviceOrderDetail.ServiceOrderDetailSurgical
            If item.RateManualDetailSurgicalId IsNot Nothing Then
                Dim rateManualDetalSurgical = _rateManualDetailSurgicalRepository.GetRateManualDetailSurgicalById(item.RateManualDetailSurgicalId)
                If serviceOrderDetail.SurchargeApply = True Then
                    costValueItem = rateManualDetalSurgical.SalesValueWithSurcharge * item.InvoicedQuantity
                Else
                    costValueItem = rateManualDetalSurgical.SalesValue * item.InvoicedQuantity
                End If
                Dim totalSalesPriceItem = costValueItem + (costValueItem * (rateVariation / 100))
                totalSalesPriceItem = Utils.RoundValue(totalSalesPriceItem, rateManualDetalSurgical.RateManual.RoundService)
                item.RateManualSalePrice = totalSalesPriceItem
                item.TotalSalesPrice = totalSalesPriceItem
                item.RoundService = rateManualDetalSurgical.RateManual.RoundService
            Else
                Dim ipsTmp = _ipsServiceRepository.GetIPSServiceById(item.IPSServiceId)

                If item.ClassServiceIps = ResourceManager.GetString("RightRoom", "Contract") AndAlso result.ObjectEmbbeded.LiquidationType <> 1 Then
                    costValueItem = IPSService.UVRNumber * ipsTmp.NewScore
                Else
                    If IPSService.UVRNumber > UVRNUMBER AndAlso ipsTmp.ApplyChangeScore Then
                        costValueItem = IPSService.UVRNumber * ipsTmp.NewScore
                    ElseIf result.ObjectEmbbeded.LiquidationType = 1 Then
                        costValueItem = item.RateManualSalePrice
                    Else
                        costValueItem = IPSService.UVRNumber * ipsTmp.Score
                    End If
                End If

                Dim totalSalesPriceItem = costValueItem + (costValueItem * (rateVariation / 100))
                totalSalesPriceItem = Utils.RoundValue(totalSalesPriceItem, rateManual.RoundService)
                item.RateManualSalePrice = totalSalesPriceItem
                item.TotalSalesPrice = totalSalesPriceItem
                item.RoundService = rateManual.RoundService
            End If
            item.SurchargeApply = True
        Next

        '/**************--Segmento Impuestos--**********************/
        'se suman el detalle de los qx
        serviceOrderDetail.SubTotalSalesPrice = serviceOrderDetail.ServiceOrderDetailSurgical.Sum(Function(x) x.TotalSalesPrice)
        'independientmente si viene de detalles qx o no al final en base a la parametrizacion del servicio y el sistema se establece el valor bruto, el iva y el subtotal
        Dim _dictionaryValues = Utils.SetValueSalesPrice(SalePriceIncludeTax,
                                                              serviceOrderDetail.SubTotalSalesPrice,
                                                              If(IPSService?.IVAId Is Nothing, 0, IPSService?.GeneralLedgerIVA?.Percentage))
        If IPSService?.TaxedProduct AndAlso _dictionaryValues?.Any() Then
            With serviceOrderDetail
                .GrossValue = _dictionaryValues?.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.GrossValue).Value
                .TaxValue = _dictionaryValues?.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.TaxValue).Value
                .SubTotalSalesPrice = _dictionaryValues?.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.SubtotalSalesPrice).Value
                If .SubTotalSalesPrice > serviceOrderDetail.RoundService Then
                    .SubTotalSalesPrice = Utils.RoundValue(serviceOrderDetail.SubTotalSalesPrice, serviceOrderDetail.RoundService)
                End If
            End With
        End If
        serviceOrderDetail.TaxedService = IPSService?.TaxedProduct
        serviceOrderDetail.TaxPercent = If(IPSService?.IVAId Is Nothing, 0, IPSService?.GeneralLedgerIVA?.Percentage)
        '/*****************************************/
        serviceOrderDetail.TotalSalesPrice = serviceOrderDetail.SubTotalSalesPrice
        serviceOrderDetail.GrandTotalSalesPrice = serviceOrderDetail.TotalSalesPrice * serviceOrderDetail.InvoicedQuantity
        serviceOrderDetail.RoundService = 1
        Return serviceOrderDetail
    End Function

    ''' <summary>
    ''' Obtiene el precio del servicio en el manual tarifario para cada una de las estancias
    ''' </summary>
    ''' <param name="caregroupId">Id del grupo de atención</param>
    ''' <param name="staysList">Lista de estancias a calcular. Las estancias deben ir con agregados</param>
    ''' <param name="collectStay">Valor que indica si se usa el CUPS de estancia o de observación</param>
    ''' <returns>La lista de estancias con el precio de cada servicio</returns>
    Public Function GetPriceToStaysList(ByVal caregroupId As Integer,
                                        ByVal staysList As List(Of CHREGESTA),
                                        stayOption As eLiquidateStayOption,
                                        Optional collectStay As Boolean = True,
                                        Optional endDate As Date? = Nothing) As ActionResult(Of List(Of CHREGESTA)) Implements IBillingServices.GetPriceToStaysList
        Try
            Dim result As New ActionResult(Of List(Of CHREGESTA))() With {.StateResult = True}

            For Each stay In staysList
                Dim cupsId As Integer = 0
                Dim CUPSEntityContractDescriptionId As Integer? = Nothing
                'Verifico que haya una tarifa establecida para la cama
                Dim query = From e In stay.CHCAMASHO.CHGENTARI
                            Where e.CHTIPESTA.CODTIPEST = stay.CHTIPESTA.CODTIPEST _
                                And e.TIPLIQEST = stay.CHCAMASHO.CODCLACAM _
                                And e.ADCENATEN.CODCENATE = stay.ADINGRESO.CODCENATE _
                                And e.UFUCODIGO = stay.CHCAMASHO.INUNIFUNC.UFUCODIGO
                            Select e
                If query.Count() = 0 Then
                    result.Message = "No se encontro una tarifa establecida para la cama " & stay.CHCAMASHO.NUMCAMHOS
                    result.StateResult = False
                    Return result
                End If

                If collectStay Then 'Si se va cobrar Hospitalizacion
                    cupsId = query.SingleOrDefault.GENCUPS2
                    CUPSEntityContractDescriptionId = query.SingleOrDefault.IDDESCRIPCIONRELACIONADA_CUPS2
                Else 'Si se va cobrar Observacion
                    cupsId = query.SingleOrDefault.GENCUPS
                    CUPSEntityContractDescriptionId = query.SingleOrDefault.IDDESCRIPCIONRELACIONADA_CUPS
                End If

                Dim cups = _cupsRepository.FirstOrDefault(Function(m) m.Id = cupsId)

                If cups Is Nothing OrElse cups.Id = 0 Then
                    result.Message = "El CUPS con Id (" & cupsId & ") parametrizado en la tarifa de la cama, no se encuentra creado en Indigo VIE"
                    result.StateResult = False
                    Return result
                End If

                'Dim careGroup = _careGroupRepository.GetCareGroupById(caregroupId)
                'Dim careGroupRate = _careGroupRateRepository.GetCareGroupRateByCareGroupCupsEntity(caregroupId, cupsId)

                Dim functionalUnitId As Integer? = Nothing
                If stay.CHCAMASHO IsNot Nothing Then
                    Dim functionalUnit As Domain.Payroll.Entities.FunctionalUnit = _functionalUnitRepository.GetFunctionalUnit(stay.CHCAMASHO.UFUCODIGO.Trim())
                    If functionalUnit IsNot Nothing AndAlso functionalUnit.Id > 0 Then
                        functionalUnitId = functionalUnit.Id
                    Else
                        result.Message = String.Format("La unidad funcional {0} no está homologada en Indigo Vie", stay.CHCAMASHO.UFUCODIGO.Trim())
                        result.StateResult = False
                        Return result
                    End If
                End If

                Dim ContractDescriptionId As Integer? = Nothing
                If CUPSEntityContractDescriptionId IsNot Nothing Then
                    ContractDescriptionId = _cupsRepository.GetContractDescriptionIdByCupsEntityContractDescription(CUPSEntityContractDescriptionId)
                End If

                '' para que pueda encontrar reglas tipo "Servicio IPS" además de las tipo "CUPS".
                Dim rate As ActionResult(Of DefinitionRateDetail, DefinitionRateDetailCondition) = Nothing
                Dim listCupsHomologation = _cupsHomologation.ListCupsHomologationByCupsId(cupsId, 0)
                If listCupsHomologation IsNot Nothing AndAlso listCupsHomologation.Any() Then
                    For Each homol In listCupsHomologation
                        rate = _contractServices.GetRateValue(homol.IPSServiceId, cupsId, caregroupId, functionalUnitId, Nothing, stay.FECINIEST.Date, ERateOptions.IPSSERVICE, Nothing, ContractDescriptionId)
                        If rate IsNot Nothing AndAlso rate.StateResult Then
                            Exit For
                        End If
                    Next
                End If
                If rate Is Nothing OrElse Not rate.StateResult Then
                    rate = _contractServices.GetRateValue(0, cupsId, caregroupId, functionalUnitId, Nothing, stay.FECINIEST.Date, ERateOptions.IPSSERVICE, Nothing, ContractDescriptionId)
                End If
                Dim liquidationType As Integer = 0
                Dim manualType As Integer = 0 'Solo se llena si es fija
                Dim salesValue As Decimal = 0 'Solo se llena si es fija
                Dim salesValueWithSurcharge As Decimal = 0 'Solo se llena si es fija
                Dim rateManual As RateManual = Nothing 'Solo se llena si es Estandar
                Dim rateVariation As Decimal = 0 'Solo se llena si es Estandar
                If rate.StateResult Then
                    If rate.ObjectEmbbededAux IsNot Nothing Then 'Fue por que la tarifa se saco de una condicion
                        Dim Validate = ValidateDefinitionRateDetail(rate?.ObjectEmbbededAux)
                        If Validate Is Nothing OrElse Not Validate.StateResult Then
                            result.Message = $"{Validate.Message}"
                            result.StateResult = False
                            Return result
                        End If
                        liquidationType = rate.ObjectEmbbededAux.LiquidationType
                        If liquidationType = 1 Then 'Si es Fija
                            manualType = rate.ObjectEmbbededAux.ManualType
                            salesValue = rate.ObjectEmbbededAux.SalesValue
                            salesValueWithSurcharge = rate.ObjectEmbbededAux.SalesValueWithSurcharge
                        ElseIf liquidationType = 2 Then 'Si es estandar
                            rateVariation = rate.ObjectEmbbededAux.RateVariation
                            rateManual = _rateManualRepository.GetRateManualById(rate.ObjectEmbbededAux.RateManualId)
                        ElseIf liquidationType = 3 Then 'Si es por vigencia
                            rateVariation = rate.ObjectEmbbededAux.RateVariation
                            Dim rateManualValidity = _rateManualValidityRepository.GetRateManualValidityByIdWithAggregates(rate.ObjectEmbbededAux.RateManualValidityId)
                            Dim rateManualValidityDetail = rateManualValidity.RateManualValidityDetail.Where(Function(d) d.InitialDate <= stay.FECINIEST.Date AndAlso d.EndDate >= stay.FECINIEST.Date).FirstOrDefault
                            If rateManualValidityDetail Is Nothing Then
                                result.Message = String.Format("La estancia de la unidad funcional {0} no tiene parametrizado un manual de tarifas para la vigencia {1} - {2} en la fecha del servicio {3}", stay.CHCAMASHO.UFUCODIGO.Trim(), rateManualValidity.Code, rateManualValidity.Name, stay.FECINIEST.Date.ToString("yyyy-mm-dd"))
                                result.StateResult = False
                                Return result
                            End If
                            rateManual = _rateManualRepository.GetRateManualById(rateManualValidityDetail.RateManualId)
                        End If
                    Else 'Es por que la condicion fue ninguna y la tarifa esta en la cabecera
                        Dim Validate = ValidateDefinitionRateDetail(rate?.ObjectEmbbeded)
                        If Validate Is Nothing OrElse Not Validate.StateResult Then
                            result.Message = $"{Validate.Message}"
                            result.StateResult = False
                            Return result
                        End If
                        liquidationType = rate.ObjectEmbbeded.LiquidationType
                        If liquidationType = 1 Then 'Si es Fija
                            manualType = rate.ObjectEmbbeded.ManualType
                            salesValue = rate.ObjectEmbbeded.SalesValue
                            salesValueWithSurcharge = rate.ObjectEmbbeded.SalesValueWithSurcharge
                        ElseIf liquidationType = 2 Then 'Si es estandar
                            rateVariation = rate.ObjectEmbbeded.RateVariation
                            rateManual = _rateManualRepository.GetRateManualById(rate.ObjectEmbbeded.RateManualId)
                        ElseIf liquidationType = 3 Then 'Si es por vigencia
                            rateVariation = rate.ObjectEmbbeded.RateVariation
                            Dim rateManualValidity = _rateManualValidityRepository.GetRateManualValidityByIdWithAggregates(rate.ObjectEmbbeded.RateManualValidityId)
                            Dim rateManualValidityDetail = rateManualValidity.RateManualValidityDetail.Where(Function(d) d.InitialDate <= stay.FECINIEST.Date AndAlso d.EndDate >= stay.FECINIEST.Date).FirstOrDefault
                            If rateManualValidityDetail Is Nothing Then
                                result.Message = String.Format("La estancia de la unidad funcional {0} no tiene parametrizado un manual de tarifas para la vigencia {1} - {2} en la fecha del servicio {3}", stay.CHCAMASHO.UFUCODIGO.Trim(), rateManualValidity.Code, rateManualValidity.Name, stay.FECINIEST.Date.ToString("yyyy-mm-dd"))
                                result.StateResult = False
                                Return result
                            End If
                            rateManual = _rateManualRepository.GetRateManualById(rateManualValidityDetail.RateManualId)
                        End If
                    End If
                Else
                    Return New ActionResult(Of List(Of CHREGESTA)) With {.StateResult = False, .Message = rate.Message}
                End If
                If liquidationType = 1 Then 'El tipo de liquidacion es Fija
                    Dim listHomologation = _cupsHomologation.GetCupsHomologationIpsServiceByCupsEntityIdAndServiceManual(cupsId, manualType)
                    If listHomologation.Count = 0 Then
                        result.Message = "El CUPS " & cups.Code & " - " & cups.Description & " No tiene homologacion con servicios de Tipo " & manualType 'Asignar Funcion Roldan
                        result.StateResult = False
                        Return result
                    End If
                    stay.CupsId = cupsId
                    stay.CUPSEntityContractDescriptionId = CUPSEntityContractDescriptionId
                    stay.IPSServiceId = listHomologation(0).IPSServiceId
                    stay.Value = salesValue
                Else 'El tipo de liquidacion es Estandar
                    Dim listHomologation = _cupsHomologation.GetCupsHomologationIpsServiceByCupsEntityIdAndServiceManual(cupsId, rateManual.Type)
                    If listHomologation.Count = 0 Then
                        result.Message = "El CUPS " & cups.Code & " - " & cups.Description & " No tiene homologacion con servicios de Tipo " & rateManual.Type  'Asignar Funcion Roldan
                        result.StateResult = False
                        Return result
                    End If
                    Dim rateManualDetail = _rateManualDetailRepository.GetRateManualDetailServiceOrder(rateManual.Id, listHomologation(0).IPSServiceId)
                    If rateManualDetail Is Nothing Then
                        result.Message = "No se encontro tarifa para el Item " & cups.Description & " en el manual tarifario " & rateManual.Name
                        result.StateResult = False
                        Return result
                    End If
                    stay.RateManualId = rateManual.Id
                    stay.RateManualDetailId = rateManualDetail.Id
                    stay.RateManualType = rateManual.Type
                    stay.CupsId = cupsId
                    stay.CUPSEntityContractDescriptionId = CUPSEntityContractDescriptionId
                    stay.IPSServiceId = listHomologation(0).IPSServiceId
                    stay.Value = rateManualDetail.SalesValue + (rateManualDetail.SalesValue * rateVariation / 100)
                End If
            Next

            Dim staysReturn As List(Of CHREGESTA) = staysList

            If stayOption = eLiquidateStayOption.CamaUltimoIngreso Then
                If staysList.Count > 1 Then
                    staysReturn = staysList _
                        .Where(Function(o) endDate Is Nothing _
                            OrElse (o.FECINIEST <= endDate AndAlso o.FECFINEST >= endDate)) _
                        .OrderBy(Function(o) o.FECINIEST) _
                        .ToList()

                    ' Esto como que soluciona algo
                    'staysReturn = staysList _
                    '    .Where(Function(o) endDate Is Nothing _
                    '        OrElse (o.FECINIEST <= endDate AndAlso New Date(IIf(o.FECFINEST.Year = 1900, 9999, o.FECFINEST.Year), o.FECFINEST.Month, o.FECFINEST.Day, 23, 59, 59) >= endDate)) _
                    '    .OrderBy(Function(o) o.FECINIEST) _
                    '    .ToList()

                    If staysReturn.Any() Then
                        staysReturn.RemoveRange(0, staysReturn.Count - 1)
                    End If
                End If
            End If

            Return New ActionResult(Of List(Of CHREGESTA)) With {.StateResult = True, .ObjectEmbbeded = staysReturn}
        Catch ex As Exception
            Return New ActionResult(Of List(Of CHREGESTA)) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    Private Function ValidateDefinitionRateDetail(_definitionRateDetail As Object) As ActionResult
        Try
            If _definitionRateDetail Is Nothing OrElse _definitionRateDetail?.LiquidationType Is Nothing Then
                Return New ActionResult With {.StateResult = False, .Message = $"{If(_definitionRateDetail Is Nothing, "La definición de tarifa esta vacia", "El tipo de liquidación esta vacia")}"}
            End If
            If _definitionRateDetail.LiquidationType = 1 AndAlso (_definitionRateDetail.ManualType Is Nothing OrElse _definitionRateDetail.SalesValue Is Nothing) Then
                Return New ActionResult With {.StateResult = False, .Message = $"{If(_definitionRateDetail.ManualType Is Nothing, "El tipo de manual esta vacio", "El valor de la tarifa esta vacia")}"}
            End If
            Return New ActionResult With {.StateResult = True}
        Catch ex As Exception
            Return New ActionResult With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Resuelve la lógica para obtener la cuenta contable de ingreso para las ordenes de servicio
    ''' </summary>
    ''' <param name="serviceOrderDetail">The service order detail.</param>
    ''' <returns></returns>
    Private Function GetIncomeMainAccount(serviceOrderDetail As ServiceOrderDetail, CareGroupId As Integer, Optional careGroup As CareGroup = Nothing) As ActionResult Implements IBillingServices.GetIncomeMainAccount
        If careGroup Is Nothing Then
            careGroup = _careGroupRepository.GetCareGroupById(CareGroupId)
        End If
        If serviceOrderDetail.RecordType = 1 Then
            'Es Servicio
            Dim CUPSEntity = _cupsRepository.GetCupsEntityById(serviceOrderDetail.CUPSEntityId)

            'Si el tipo de contabilizacion es 2 entonces miramos el tipo de unidad de la unidad funcional asociada al detalle de orden de servicio
            'y luego se busca en los detalles de parametros de facturación y de allí se toman las cuentas
            Dim bConcept As BillingConcept = CUPSEntity.BillingConcept 'GetBillingConcept(cupsEntity.BillingConceptId)
            If bConcept.AccountingType = 2 Then
                'Cuenta por Tipo de Unidad Nota
                Dim entity As Boolean = True
                If careGroup.CareGroupType = eCareGroupType.Particular Then
                    entity = False
                End If
                Dim funcUnit As Object
                If serviceOrderDetail.FunctionalUnit Is Nothing Then
                    funcUnit = _functionalUnitRepository.GetFunctionalUnitById(serviceOrderDetail.PerformsFunctionalUnitId, False)
                    If funcUnit Is Nothing OrElse funcUnit.Id = 0 Then
                        Return New ActionResult With {.StateResult = False, .Message = "La unidad funcional no se encuentra homologada en Indigo Vie"}
                    End If
                Else
                    funcUnit = serviceOrderDetail.FunctionalUnit
                End If


                Dim unitType As Byte = IIf({1, 23}.Contains(funcUnit.UnitType), 1,
                                           IIf({2, 5, 6, 7, 8, 9, 10, 11, 16, 17, 18}.Contains(funcUnit.UnitType), 2,
                                               IIf({19}.Contains(funcUnit.UnitType), 3, IIf({3, 4, 12, 13, 14, 15, 20, 21, 22, 24, 25}.Contains(funcUnit.UnitType), 4, 0))))

                'Se valida que hayan diligenciado las cuentas en el detalle de los conceptos de facturación
                If bConcept.BillingConceptAccount Is Nothing OrElse bConcept.BillingConceptAccount.Count = 0 Then
                    Return New ActionResult With {.StateResult = False, .Message = "No estan parametrizadas las cuentas en el detalle del concepto " + bConcept.Code + " - " + bConcept.Name}
                End If

                If entity Then
                    serviceOrderDetail.IncomeMainAccountId = bConcept.BillingConceptAccount.Where(Function(o) o.UnitType = unitType).FirstOrDefault().EntityIncomeAccountId
                Else
                    serviceOrderDetail.IncomeMainAccountId = bConcept.BillingConceptAccount.Where(Function(o) o.UnitType = unitType).FirstOrDefault().IndividualIncomeAccountId
                End If
            Else
                Select Case careGroup.CareGroupType
                    Case eCareGroupType.EAPBWithContract, eCareGroupType.EAPBWithOutContract, eCareGroupType.Aseguradora
                        serviceOrderDetail.IncomeMainAccountId = bConcept.EntityIncomeAccountId
                    Case eCareGroupType.Particular
                        serviceOrderDetail.IncomeMainAccountId = bConcept.IndividualIncomeAccountId
                End Select
            End If
        ElseIf serviceOrderDetail.RecordType = 2 Then
            'Es Producto
            Dim productGroup As ProductGroup = Nothing
            productGroup = _productGroupRepository.GetProductGroupByProductId(CInt(serviceOrderDetail.ProductId))

            If productGroup Is Nothing OrElse productGroup.Id = 0 Then
                Dim FunctionalUnit = _functionalUnitRepository.GetFunctionalUnitById(serviceOrderDetail.PerformsFunctionalUnitId)
                Return New ActionResult With {.StateResult = False, .Message = String.Format(ResourceManager.GetString("SettingParameterNotFountFunctionalUnit"), String.Concat(FunctionalUnit.Code, " - ", FunctionalUnit.Name))}
            Else
                serviceOrderDetail.IncomeMainAccountId = productGroup.IncomeAccountId
            End If
        End If

        If serviceOrderDetail.ServiceOrderDetailSurgical.Count > 0 Then
            For Each surgical In serviceOrderDetail.ServiceOrderDetailSurgical
                Dim bConcept As BillingConcept = _billingConceptRepository.GetBillingConceptById(surgical.BillingConceptId, False)
                If bConcept.ConceptType = 1 Then
                    'Facturación básica
                    Return New ActionResult With {.StateResult = False, .Message = String.Format("El concepto de facturación {0} no puede ser de tipo facturación básica", bConcept.Code)}
                End If
                If bConcept.AccountingType = 2 Then
                    'Cuenta por Tipo de Unidad Nota
                    Dim entity As Boolean = True
                    If careGroup.CareGroupType = eCareGroupType.Particular Then
                        entity = False
                    End If

                    Dim funcUnit As Object
                    If serviceOrderDetail.FunctionalUnit Is Nothing Then
                        funcUnit = _functionalUnitRepository.GetFunctionalUnitById(serviceOrderDetail.PerformsFunctionalUnitId, False)
                        If funcUnit Is Nothing OrElse funcUnit.Id = 0 Then
                            Return New ActionResult With {.StateResult = False, .Message = "La unidad funcional no se encuentra homologada en Indigo Vie"}
                        End If
                    Else
                        funcUnit = serviceOrderDetail.FunctionalUnit
                    End If

                    Dim unitType As Byte = IIf({1, 23}.Contains(funcUnit.UnitType), 1,
                                               IIf({2, 5, 6, 7, 8, 9, 10, 11, 16, 17, 18}.Contains(funcUnit.UnitType), 2,
                                                   IIf({19}.Contains(funcUnit.UnitType), 3, IIf({3, 4, 12, 13, 14, 15, 20, 21, 22, 24, 25}.Contains(funcUnit.UnitType), 4, 0))))
                    If entity Then
                        surgical.IncomeMainAccountId = bConcept.BillingConceptAccount.Where(Function(o) o.UnitType = unitType).FirstOrDefault().EntityIncomeAccountId
                    Else
                        surgical.IncomeMainAccountId = bConcept.BillingConceptAccount.Where(Function(o) o.UnitType = unitType).FirstOrDefault().IndividualIncomeAccountId
                    End If
                Else
                    Select Case careGroup.CareGroupType
                        Case eCareGroupType.EAPBWithContract, eCareGroupType.EAPBWithOutContract, eCareGroupType.Aseguradora
                            surgical.IncomeMainAccountId = bConcept.EntityIncomeAccountId
                        Case eCareGroupType.Particular
                            surgical.IncomeMainAccountId = bConcept.IndividualIncomeAccountId
                    End Select
                End If

            Next
        End If
        Return New ActionResult With {.StateResult = True}
    End Function

#End Region

#Region "Liquidation Methods"

    ''' <summary>
    ''' Processes the liquidate recovery.
    ''' </summary>
    Private Function ProcessLiquidateRecovery(serv As ServiceOrderDetailDistribution, recoveryFeeType As Integer, topEvenValue As Decimal, nivelTopValue As Decimal, nivelApplyPercentage As Decimal, currentAmmountAnual As Decimal, ammountAnualTop As Decimal, LiquidationType As eLiquidateRecoveryType, liquidate As Boolean) As ActionResult
        Dim result As New ActionResult
        If (LiquidationType = eLiquidateRecoveryType.OnlyOne AndAlso (serv.ApplyRecoveryFee = 0 OrElse serv.ApplyRecoveryFee = 1)) OrElse (LiquidationType = eLiquidateRecoveryType.AllItems AndAlso (serv.ApplyRecoveryFee = 0 OrElse serv.ApplyRecoveryFee = 1)) OrElse (LiquidationType = eLiquidateRecoveryType.MultiSelectItems AndAlso (serv.ApplyRecoveryFee = 0 OrElse serv.ApplyRecoveryFee = 1) AndAlso liquidate = True) Then
            'Se debe tener en cuenta el tope del evento almacenado en RevenueControl

            If topEvenValue < nivelTopValue Then
                Dim val As Decimal = 0
                If recoveryFeeType = 3 Then
                    'Copago
                    val = (serv.GrandTotalSalesPrice * (nivelApplyPercentage / 100))
                ElseIf recoveryFeeType = 2 Then
                    'Cuota Moderadora
                    val = nivelTopValue
                End If

                If (topEvenValue + val) > nivelTopValue Then
                    val = val - ((topEvenValue + val) - nivelTopValue)
                End If
                If ammountAnualTop > 0 Then
                    If val <= (ammountAnualTop - currentAmmountAnual) Then
                        result.StateResult = True
                    Else
                        val = (ammountAnualTop - currentAmmountAnual)
                        result.StateResult = False
                        If val > 0 Then
                            result.Message = String.Format("Solo se liquido la cantidad de {0} debido a que se completo el tope anual", val.ToString("C0"))
                        Else
                            result.Message = "Se completó el tope maximo anual"
                        End If
                    End If
                Else
                    result.StateResult = True
                End If
                If val > 0 Then
                    'If (val + topEvenValue) >= nivelTopValue Then
                    '    serv.SubTotalPatientSalesPrice = Math.Round(val, 0) 'Asignamos el valor calculado de copago para el servicio
                    'Else
                    '    serv.SubTotalPatientSalesPrice = Math.Round(val, 0) 'Asignamos el valor calculado de copago para el servicio
                    'End If
                    serv.SubTotalPatientSalesPrice = Math.Round(val, 0) 'Asignamos el valor calculado de copago para el servicio
                    serv.RecoveryFeeType = recoveryFeeType 'Copago
                    serv.ApplyRecoveryFee = 2 'Se cobro
                    serv.PatientPercentage = nivelApplyPercentage 'Porcentaje aplicado
                    serv.ThirdPartySalesPrice = serv.GrandTotalSalesPrice - serv.SubTotalPatientSalesPrice 'Se calcula el valor de la entidad
                    serv.ThirdPartyPercentage = 100 - serv.PatientPercentage
                End If
            Else 'Se supera el tope del evento para el ingreso
                serv.SubTotalPatientSalesPrice = 0 'Valor cero
                serv.RecoveryFeeType = 1 'Ninguna
                serv.ApplyRecoveryFee = 1 'Disponible para cobrar
                serv.PatientPercentage = 0 'Porcentaje aplicado
                serv.ThirdPartySalesPrice = serv.GrandTotalSalesPrice - serv.SubTotalPatientSalesPrice 'Se calcula el valor de la entidad
                serv.ThirdPartyPercentage = 100
                result.StateResult = False
                If recoveryFeeType = 2 Then
                    result.Message = ResourceManager.GetString("LiquidationMessage3", MODULE_NAME)
                ElseIf recoveryFeeType = 3 Then
                    result.Message = ResourceManager.GetString("LiquidationMessage2", MODULE_NAME)
                ElseIf recoveryFeeType = 5 Then
                    result.Message = ResourceManager.GetString("LiquidationMessage6", MODULE_NAME)
                End If
            End If


        ElseIf (LiquidationType = eLiquidateRecoveryType.OnlyOne AndAlso serv.ApplyRecoveryFee = 2) Then
            serv.SubTotalPatientSalesPrice = 0 'Valor cero
            serv.RecoveryFeeType = 1 'Ninguna
            serv.ApplyRecoveryFee = 0 'No se cobra
            serv.PatientPercentage = 0 'Porcentaje aplicado
            serv.ThirdPartySalesPrice = serv.GrandTotalSalesPrice - serv.SubTotalPatientSalesPrice 'Se calcula el valor de la entidad
            serv.ThirdPartyPercentage = 100
            result.StateResult = True
        ElseIf LiquidationType = eLiquidateRecoveryType.MultiSelectItems AndAlso liquidate = False Then
            serv.SubTotalPatientSalesPrice = 0 'Valor cero
            serv.RecoveryFeeType = 1 'Ninguna
            serv.ApplyRecoveryFee = 1 'Disponible para cobrar
            serv.PatientPercentage = 0 'Porcentaje aplicado
            serv.ThirdPartySalesPrice = serv.GrandTotalSalesPrice - serv.SubTotalPatientSalesPrice 'Se calcula el valor de la entidad
            serv.ThirdPartyPercentage = 100
            result.StateResult = True
        ElseIf LiquidationType = eLiquidateRecoveryType.AllItems AndAlso liquidate = False Then
            serv.SubTotalPatientSalesPrice = 0 'Valor cero
            serv.RecoveryFeeType = 1 'Ninguna
            serv.ApplyRecoveryFee = 1 'Disponible para cobrar
            serv.PatientPercentage = 0 'Porcentaje aplicado
            serv.ThirdPartySalesPrice = serv.GrandTotalSalesPrice - serv.SubTotalPatientSalesPrice 'Se calcula el valor de la entidad
            serv.ThirdPartyPercentage = 100
            result.StateResult = True
        Else
            result.StateResult = True
        End If
        Return result
    End Function

    Private Function LiquidatePatientShare(serviceorderDetailDistributionListId As List(Of Integer), folio As RevenueControlDetail, admission As Object, iNPACIENTTOPANU As INPACIENTTOPANU, LiquidationType As eLiquidateRecoveryType, Optional liquidate As Boolean = False, Optional args As Object = Nothing) As ActionResult
        'se obtiene el valor redondeado
        Dim valueProcessRecoveryFee As Decimal = 0
        Dim typeRecoveryFeeProcess As Integer = 3 'copago
        Dim percentageProcessRecoveryFee As Decimal = 0
        Dim valueProcess As Decimal = 0
        'si se seleccionaron algunos items en la rejilla se toma el valor de esos
        If args.TotalsItemsApplyRecoveryFee > 0 Then
            valueProcess = args.TotalsItemsApplyRecoveryFee
        Else 'se toma el valor del folio
            valueProcess = folio.TotalFolio
        End If
        Select Case admission.PatientType
            Case 1 'contributivo
                'se obtiene si se debe pagar cuota moderadora o copago
                Select Case admission.LiquidationType
                    Case 1 'copago
                        typeRecoveryFeeProcess = 3
                    Case 2 'cuota moderadora
                        typeRecoveryFeeProcess = 2
                    Case 4 'Si puede ser Copago o cuota moderadora lo define entonces el tipo del ingreso
                        'If admission.AdmissionType = 1 Then 'Si el ingreso es Abulatorio cobro Cuota Moderadora
                        '    typeRecoveryFeeProcess = 2
                        'Else 'Si es Hospitalario Cobro Copago
                        '    typeRecoveryFeeProcess = 3
                        'End If
                        Return LiquidatePatientByEvent(serviceorderDetailDistributionListId, folio, admission,
                                        iNPACIENTTOPANU, LiquidationType, liquidate, args)
                End Select

                If typeRecoveryFeeProcess = 2 Then 'cuota moderadora
                    valueProcessRecoveryFee = admission.NivelModeratorShareTop
                    'solo si el tope anual es mayor a 0
                    If admission.NivelModeratorShareTopYear > 0 Then
                        If valueProcessRecoveryFee + iNPACIENTTOPANU.PAGADOCMO > admission.NivelModeratorShareTopYear Then
                            valueProcessRecoveryFee = admission.NivelModeratorShareTopYear - iNPACIENTTOPANU.PAGADOCMO
                        End If
                        If valueProcessRecoveryFee < 0 Then
                            Return New ActionResult With {.Message = "Se alcanzo el tope maximo para pagar en el año", .StateResult = False}
                        End If
                    End If
                Else 'copago
                    valueProcessRecoveryFee = (valueProcess * admission.NivelCoPayContribPercentage) / 100
                    percentageProcessRecoveryFee = admission.NivelCoPayContribPercentage
                    If admission.NivelCoPayContribPercentage > 0 Then
                        'If valueProcessRecoveryFee > admission.NivelCoPayContribTopYear Then
                        '    Return New ActionResult With {.Message = "Se alcanzo el tope maximo para pagar en el año, Tope:" + admission.NivelCoPayContribTopYear.ToString() + " Valor Cuota:" + valueProcessRecoveryFee.ToString, .StateResult = False}
                        'End If
                        If valueProcessRecoveryFee > admission.NivelCoPayContribTop Then
                            valueProcessRecoveryFee = admission.NivelCoPayContribTop
                        End If
                        If valueProcessRecoveryFee + iNPACIENTTOPANU.PAGADOCOP > admission.NivelCoPayContribTopYear Then
                            valueProcessRecoveryFee = admission.NivelCoPayContribTopYear - iNPACIENTTOPANU.PAGADOCOP
                        End If
                        If valueProcessRecoveryFee < 0 Then
                            Return New ActionResult With {.Message = "Se alcanzo el tope maximo para pagar en el año, Tope:" + admission.NivelCoPayContribTopYear.ToString() + " Valor Cuota:" + valueProcessRecoveryFee.ToString, .StateResult = False}
                        End If
                    Else
                        If valueProcessRecoveryFee > admission.NivelCoPayContribTop Then
                            valueProcessRecoveryFee = admission.NivelCoPayContribTop
                        End If
                    End If
                End If
            Case 2 'subsidiado
                valueProcessRecoveryFee = (valueProcess * admission.NivelCoPaySubsiPercentage) / 100
                percentageProcessRecoveryFee = admission.NivelCoPaySubsiPercentage
                If admission.NivelCoPaySubsibTopYear > 0 Then
                    If valueProcessRecoveryFee > admission.NivelCoPaySubsibTop Then
                        valueProcessRecoveryFee = admission.NivelCoPaySubsibTop
                    End If
                    If valueProcessRecoveryFee + iNPACIENTTOPANU.PAGADOCOP > admission.NivelCoPaySubsibTopYear Then
                        valueProcessRecoveryFee = admission.NivelCoPaySubsibTopYear - iNPACIENTTOPANU.PAGADOCOP
                    End If
                    If valueProcessRecoveryFee < 0 Then
                        Return New ActionResult With {.Message = "Se alcanzo el tope maximo para pagar en el año", .StateResult = False}
                    End If
                Else
                    If valueProcessRecoveryFee > admission.NivelCoPaySubsibTop Then
                        valueProcessRecoveryFee = admission.NivelCoPaySubsibTop
                    End If
                End If
            Case 3 'vinculado
                valueProcessRecoveryFee = (valueProcess * admission.NivelCoPayVincuPercentage) / 100
                percentageProcessRecoveryFee = admission.NivelCoPayVincuPercentage
                If admission.NivelCoPayVincuTopYear > 0 Then
                    If valueProcessRecoveryFee > admission.NivelCoPayVincuTop Then
                        valueProcessRecoveryFee = admission.NivelCoPayVincuTop
                    End If
                    If valueProcessRecoveryFee + iNPACIENTTOPANU.PAGADOCOP > admission.NivelCoPayVincuTopYear Then
                        valueProcessRecoveryFee = admission.NivelCoPayVincuTopYear - iNPACIENTTOPANU.PAGADOCOP
                    End If
                    If valueProcessRecoveryFee < 0 Then
                        Return New ActionResult With {.Message = "Se alcanzo el tope maximo para pagar en el año", .StateResult = False}
                    End If
                Else
                    If valueProcessRecoveryFee > admission.NivelCoPayVincuTop Then
                        valueProcessRecoveryFee = admission.NivelCoPayVincuTop
                    End If
                End If
            Case Else
        End Select

        If valueProcessRecoveryFee = 0 Then
            Return New ActionResult With {.Message = "Se alcanzo el tope maximo para pagar en el año", .StateResult = False}
        End If

        valueProcessRecoveryFee = Utils.RoundValue(valueProcessRecoveryFee, Utils.RoundLevel.Hundred)
        If typeRecoveryFeeProcess = 2 Then 'cuota moderadora
            folio.RevenueControl.TopEventFeeModerator += valueProcessRecoveryFee
        Else 'copago
            folio.RevenueControl.TopEventCopay += valueProcessRecoveryFee
        End If


        Dim serv As ServiceOrderDetailDistribution = Nothing
        For Each soddId As Integer In serviceorderDetailDistributionListId
            Dim invalidId = CType(args.ListItemsApplyRecoveryFee, List(Of Object)).Find(Function(x) x = soddId)
            If invalidId = 0 Then
                Continue For
            End If
            If valueProcessRecoveryFee = 0 Then
                Exit For
            End If
            serv = _serviceOrderDetailDistributionRepository.GetServiceOrderDetailDistributionByIdWithIncludes(soddId, {"ServiceOrderDetail", "ServiceOrderDetail.IPSService"}.ToArray)

            Dim value = Utils.RoundValue((serv.GrandTotalSalesPrice * percentageProcessRecoveryFee) / 100, Utils.RoundLevel.Hundred)
            If valueProcessRecoveryFee > value Then
                serv.SubTotalPatientSalesPrice = value
                valueProcessRecoveryFee -= value
            Else
                serv.SubTotalPatientSalesPrice = valueProcessRecoveryFee
                valueProcessRecoveryFee = 0
            End If

            serv.RecoveryFeeType = typeRecoveryFeeProcess
            serv.ApplyRecoveryFee = 2 'Se cobro
            serv.PatientPercentage = percentageProcessRecoveryFee 'Porcentaje aplicado
            serv.ThirdPartySalesPrice = serv.GrandTotalSalesPrice - serv.SubTotalPatientSalesPrice 'Se calcula el valor de la entidad
            serv.ThirdPartyPercentage = 100 - serv.PatientPercentage
        Next
        If serv IsNot Nothing AndAlso valueProcessRecoveryFee > 0 Then
            serv.SubTotalPatientSalesPrice += valueProcessRecoveryFee
            serv.ThirdPartySalesPrice = serv.GrandTotalSalesPrice - serv.SubTotalPatientSalesPrice 'Se calcula el valor de la entidad
        End If
        Return New ActionResult With {.StateResult = True}
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="serviceorderDetailDistributionListId"></param>
    ''' <param name="folio"></param>
    ''' <param name="admission"></param>
    ''' <param name="iNPACIENTTOPANU"></param>
    ''' <param name="LiquidationType"></param>
    ''' <param name="liquidate"></param>
    ''' <param name="args"></param>
    ''' <returns></returns>
    Private Function LiquidatePatientByEvent(serviceorderDetailDistributionListId As List(Of Integer), folio As RevenueControlDetail, admission As Object,
                                        iNPACIENTTOPANU As INPACIENTTOPANU, LiquidationType As eLiquidateRecoveryType, Optional liquidate As Boolean = False, Optional args As Object = Nothing)
        Dim result As New ActionResult(Of RevenueControlDetail)()
        Dim totalStatus As Boolean = True
        Dim liquidationMessage2_3 As Boolean = False
        Dim mensajes As New List(Of String)

        For Each soddId As Integer In serviceorderDetailDistributionListId
            Dim serv As ServiceOrderDetailDistribution = _serviceOrderDetailDistributionRepository.GetServiceOrderDetailDistributionByIdWithIncludes(soddId, {"ServiceOrderDetail", "ServiceOrderDetail.IPSService"}.ToArray)
            'Condiciones para realizar el calculo

            If serv.ServiceOrderDetail.RecordType = 2 Then 'Si es medicamento

                If admission.AdmissionType = 1 Then 'Si el ingreso es Abulatorio cobro Cuota Moderadora
                    Dim res = ProcessLiquidateRecovery(serv, 2, folio.RevenueControl.TopEventFeeModerator, admission.NivelModeratorShareTop, admission.NivelModeratorSharePercentage, iNPACIENTTOPANU.PAGADOCMO, admission.NivelModeratorShareTopYear, LiquidationType, liquidate)
                    result.StateResult = res.StateResult
                    mensajes.Add(res.Message)
                    liquidationMessage2_3 = ContainsLiquidationMessage(res)
                    folio.RevenueControl.TopEventFeeModerator += serv.SubTotalPatientSalesPrice
                Else 'Si es Hospitalario Cobro Copago
                    Dim res = ProcessLiquidateRecovery(serv, 3, folio.RevenueControl.TopEventCopay, admission.NivelCoPayContribTop, admission.NivelCoPayContribPercentage, iNPACIENTTOPANU.PAGADOCOP, admission.NivelCoPayContribTopYear, LiquidationType, liquidate)
                    result.StateResult = res.StateResult
                    mensajes.Add(res.Message)
                    liquidationMessage2_3 = ContainsLiquidationMessage(res)
                    folio.RevenueControl.TopEventCopay += serv.SubTotalPatientSalesPrice
                End If

            Else 'Si es un servicio
                Dim recoveryFeeType As Byte = If(admission.AdmissionType = 1, serv.ServiceOrderDetail.IPSService.OutPatientRecoveryFeeType, serv.ServiceOrderDetail.IPSService.InPatientRecoveryFeeType)
                If recoveryFeeType = 3 Then 'Si es copago
                    If admission.PatientAfiliation = 1 Then 'Si es cotizante, no se calcula
                        serv.SubTotalPatientSalesPrice = 0 'Valor cero
                        serv.RecoveryFeeType = 1 'Ninguna
                        serv.ApplyRecoveryFee = 0 'No se puede cobrar
                        serv.PatientPercentage = 0 'Porcentaje aplicado
                        serv.ThirdPartySalesPrice = serv.GrandTotalSalesPrice - serv.SubTotalPatientSalesPrice 'Se calcula el valor de la entidad
                        serv.ThirdPartyPercentage = 100
                        result.StateResult = False
                        mensajes.Add(ResourceManager.GetString("LiquidationMessage1", MODULE_NAME))
                    Else 'Diferente a cotizante
                        Dim res = ProcessLiquidateRecovery(serv, 3, folio.RevenueControl.TopEventCopay, admission.NivelCoPayContribTop, admission.NivelCoPayContribPercentage, iNPACIENTTOPANU.PAGADOCOP, admission.NivelCoPayContribTopYear, LiquidationType, liquidate)
                        result.StateResult = res.StateResult
                        mensajes.Add(res.Message)
                        liquidationMessage2_3 = ContainsLiquidationMessage(res)
                        folio.RevenueControl.TopEventCopay += serv.SubTotalPatientSalesPrice
                    End If
                ElseIf recoveryFeeType = 2 Then 'Si es cuota moderadora
                    Dim res = ProcessLiquidateRecovery(serv, 2, folio.RevenueControl.TopEventFeeModerator, admission.NivelModeratorShareTop, admission.NivelModeratorSharePercentage, iNPACIENTTOPANU.PAGADOCMO, admission.NivelModeratorShareTopYear, LiquidationType, liquidate)
                    result.StateResult = res.StateResult
                    mensajes.Add(res.Message)
                    liquidationMessage2_3 = ContainsLiquidationMessage(res)
                    folio.RevenueControl.TopEventFeeModerator += serv.SubTotalPatientSalesPrice
                ElseIf recoveryFeeType = 1 Then 'El manual esta configurado en ninguno
                    serv.SubTotalPatientSalesPrice = 0 'Valor cero
                    serv.RecoveryFeeType = 1 'Ninguna
                    serv.ApplyRecoveryFee = 0 'No se puede cobrar
                    serv.PatientPercentage = 0 'Porcentaje aplicado
                    serv.ThirdPartySalesPrice = serv.GrandTotalSalesPrice - serv.SubTotalPatientSalesPrice 'Se calcula el valor de la entidad
                    serv.ThirdPartyPercentage = 100
                    result.StateResult = False
                    mensajes.Add(String.Format(ResourceManager.GetString("LiquidationMessage4", MODULE_NAME), serv.ServiceOrderDetail.IPSService.Code & "-" & serv.ServiceOrderDetail.IPSService.Name))
                Else 'No se cobra nada
                    serv.SubTotalPatientSalesPrice = 0 'Valor cero
                    serv.RecoveryFeeType = 1 'Ninguna
                    serv.ApplyRecoveryFee = 0 'No se puede cobrar
                    serv.PatientPercentage = 0 'Porcentaje aplicado
                    serv.ThirdPartySalesPrice = serv.GrandTotalSalesPrice - serv.SubTotalPatientSalesPrice 'Se calcula el valor de la entidad
                    serv.ThirdPartyPercentage = 100
                    result.StateResult = False
                    mensajes.Add(String.Format(ResourceManager.GetString("LiquidationMessage5", MODULE_NAME), serv.ServiceOrderDetail.RateManualDetail.RateManual.Code & "-" & serv.ServiceOrderDetail.RateManualDetail.RateManual.Name))
                End If
            End If

            totalStatus = totalStatus And result.StateResult
            If totalStatus = False Then
                If liquidationMessage2_3 Then
                    result.StateResult = True
                    result.Message = String.Empty
                Else
                    result.StateResult = False
                    result.Message = String.Join(vbCrLf, mensajes.Distinct().ToArray)
                End If
                result.ObjectEmbbeded = folio
                Return result
            End If
        Next
        result.Message = String.Join(vbCrLf, mensajes.Distinct().ToArray)
        result.ObjectEmbbeded = folio
        result.StateResult = totalStatus
        'Retornamos el resultado de la acción con el folio modificado cual sea las condiciones del ingreso y sus servicios
        Return result
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="mensaje"></param>
    ''' <returns></returns>
    Private Function ContainsLiquidationMessage(mensaje As ActionResult)
        Return (Not mensaje.StateResult) AndAlso (mensaje.Message.Contains(ResourceManager.GetString("LiquidationMessage2", MODULE_NAME)) OrElse mensaje.Message.Contains(ResourceManager.GetString("LiquidationMessage3", MODULE_NAME)))
    End Function


    Private Function DeleteLiquidatePatientShare(serviceorderDetailDistributionListId As List(Of Integer), folio As RevenueControlDetail) As ActionResult

        For Each soddId As Integer In serviceorderDetailDistributionListId
            Dim serv As ServiceOrderDetailDistribution = _serviceOrderDetailDistributionRepository.GetServiceOrderDetailDistributionByIdWithIncludes(soddId, {"ServiceOrderDetail", "ServiceOrderDetail.IPSService"}.ToArray)
            If serv.RecoveryFeeType = 2 Then 'cuota moderadora
                folio.RevenueControl.TopEventFeeModerator -= serv.SubTotalPatientSalesPrice
            Else 'copago
                folio.RevenueControl.TopEventCopay -= serv.SubTotalPatientSalesPrice
            End If
            serv.RecoveryFeeType = 1 'ninguna
            serv.ApplyRecoveryFee = 1 'Se cobro
            serv.PatientPercentage = 0 'Porcentaje aplicado
            serv.ThirdPartySalesPrice = serv.GrandTotalSalesPrice 'Se calcula el valor de la entidad
            serv.ThirdPartyPercentage = 100
            serv.SubTotalPatientSalesPrice = 0
        Next
        Return New ActionResult With {.StateResult = True}
    End Function

    ''' <summary>
    ''' Liquidates the recovery fee.
    ''' </summary>
    ''' <returns></returns>
    Public Function LiquidateRecoveryFee(ByVal admission As Object, ByVal idFolio As Integer, serviceOrderDetailDistributionDetail As Integer, ServiceDistributionList As List(Of Object), LiquidationType As eLiquidateRecoveryType, Optional liquidate As Boolean = False, Optional args As Object = Nothing) As ActionResult(Of RevenueControlDetail) Implements IBillingServices.LiquidateRecoveryFee
        Dim messageResult As New List(Of String)()
        Dim totalStatus As Boolean = True
        Dim result As New ActionResult(Of RevenueControlDetail)()
        Dim folio As RevenueControlDetail = Me._revenueControlDetailRepository.GetRevenueControlDetailByIdWithIncludes(idFolio, {"RevenueControl"}.ToArray)
        If folio IsNot Nothing Then
            folio.StartTracking()
            folio.RevenueControl.StartTracking()
            Dim lstServiceOrderDetailDistribution As New List(Of ServiceOrderDetailDistribution)()
            Dim serviceorderDetailDistributionListId As List(Of Integer) = Nothing
            Select Case LiquidationType
                Case eLiquidateRecoveryType.AllItems
                    If Not liquidate Then
                        serviceorderDetailDistributionListId = _serviceOrderDetailDistributionRepository.ListServiceOrderDetailDistributionIdByRevenueControlDetailIdToRemove(folio.Id)
                    Else
                        serviceorderDetailDistributionListId = _serviceOrderDetailDistributionRepository.ListServiceOrderDetailDistributionIdByRevenueControlDetailId(folio.Id)
                    End If
                Case eLiquidateRecoveryType.OnlyOne
                    serviceorderDetailDistributionListId = {serviceOrderDetailDistributionDetail}.ToList()
                Case eLiquidateRecoveryType.MultiSelectItems
                    serviceorderDetailDistributionListId = ServiceDistributionList.Select(Function(o) CType(o, Integer)).ToList()
            End Select
            Dim iNPACIENTTOPANU As INPACIENTTOPANU = _iNPACIENTTOPANURepository.GetINPACIENTTOPANUByIPCODPACI(folio.RevenueControl.PatientCode, Date.Now.Year)

            If liquidate Then
                Dim resultLiquidatePatientShare As ActionResult = LiquidatePatientShare(serviceorderDetailDistributionListId, folio, admission, iNPACIENTTOPANU, args)
                If Not resultLiquidatePatientShare.StateResult Then
                    result.Message = resultLiquidatePatientShare.Message
                    result.StateResult = False
                    Return result
                End If
                result.Message = "Cuota paciente generada correctamente"
            Else
                Dim resultDeletePatientShare As ActionResult = DeleteLiquidatePatientShare(serviceorderDetailDistributionListId, folio)
                result.Message = "Cuota paciente eliminada correctamente"
            End If

            result.ObjectEmbbeded = folio
            result.StateResult = totalStatus
            'Retornamos el resultado de la acción con el folio modificado cual sea las condiciones del ingreso y sus servicios
            Return result

            'For Each soddId As Integer In serviceorderDetailDistributionListId
            '    Dim serv As ServiceOrderDetailDistribution = _serviceOrderDetailDistributionRepository.GetServiceOrderDetailDistributionByIdWithIncludes(soddId, {"ServiceOrderDetail", "ServiceOrderDetail.IPSService"}.ToArray)
            '    'Condiciones para realizar el calculo
            '    Select Case admission.PatientType
            '        Case 1 'Constributivo
            '            If serv.ServiceOrderDetail.RecordType = 2 Then 'Si es medicamento
            '                If admission.LiquidationType = 1 Then ' Si esta parametrizado como copago
            'Dim res = ProcessLiquidateRecovery(serv, 3, folio.RevenueControl.TopEventCopay, admission.NivelCoPayContribTop, admission.NivelCoPayContribPercentage, iNPACIENTTOPANU.PAGADOCOP, admission.NivelCoPayContribTopYear, LiquidationType, liquidate)
            '                    result.StateResult = res.StateResult
            '                    messageResult.Add(res.Message)
            '                    folio.RevenueControl.TopEventCopay += serv.SubTotalPatientSalesPrice
            '                ElseIf admission.LiquidationType = 2 Then 'Si es Cuota moderadora
            '                    Dim res = ProcessLiquidateRecovery(serv, 2, folio.RevenueControl.TopEventFeeModerator, admission.NivelModeratorShareTop, admission.NivelModeratorSharePercentage, iNPACIENTTOPANU.PAGADOCMO, admission.NivelModeratorShareTopYear, LiquidationType, liquidate)
            '                    result.StateResult = res.StateResult
            '                    messageResult.Add(res.Message)
            '                    folio.RevenueControl.TopEventFeeModerator += serv.SubTotalPatientSalesPrice
            '                ElseIf admission.LiquidationType = 4 Then 'Si puede ser Copago o cuota moderadora lo define entonces el tipo del ingreso
            '                    If admission.AdmissionType = 1 Then 'Si el ingreso es Abulatorio cobro Cuota Moderadora
            '                        Dim res = ProcessLiquidateRecovery(serv, 2, folio.RevenueControl.TopEventFeeModerator, admission.NivelModeratorShareTop, admission.NivelModeratorSharePercentage, iNPACIENTTOPANU.PAGADOCMO, admission.NivelModeratorShareTopYear, LiquidationType, liquidate)
            '                        result.StateResult = res.StateResult
            '                        messageResult.Add(res.Message)
            '                        folio.RevenueControl.TopEventFeeModerator += serv.SubTotalPatientSalesPrice
            '                    Else 'Si es Hospitalario Cobro Copago
            '                        Dim res = ProcessLiquidateRecovery(serv, 3, folio.RevenueControl.TopEventCopay, admission.NivelCoPayContribTop, admission.NivelCoPayContribPercentage, iNPACIENTTOPANU.PAGADOCOP, admission.NivelCoPayContribTopYear, LiquidationType, liquidate)
            '                        result.StateResult = res.StateResult
            '                        messageResult.Add(res.Message)
            '                        folio.RevenueControl.TopEventCopay += serv.SubTotalPatientSalesPrice
            '                    End If
            '                End If
            '            Else 'Si es un servicio
            '                Dim recoveryFeeType As Byte = If(admission.AdmissionType = 1, serv.ServiceOrderDetail.IPSService.OutPatientRecoveryFeeType, serv.ServiceOrderDetail.IPSService.InPatientRecoveryFeeType)
            '                If recoveryFeeType = 3 Then 'Si es copago
            '                    If admission.PatientAfiliation = 1 Then 'Si es cotizante, no se calcula
            '                        serv.SubTotalPatientSalesPrice = 0 'Valor cero
            '                        serv.RecoveryFeeType = 1 'Ninguna
            '                        serv.ApplyRecoveryFee = 0 'No se puede cobrar
            '                        serv.PatientPercentage = 0 'Porcentaje aplicado
            '                        serv.ThirdPartySalesPrice = serv.GrandTotalSalesPrice - serv.SubTotalPatientSalesPrice 'Se calcula el valor de la entidad
            '                        serv.ThirdPartyPercentage = 100
            '                        result.StateResult = False
            '                        messageResult.Add(ResourceManager.GetString("LiquidationMessage1", MODULE_NAME))
            '                    Else 'Diferente a cotizante
            '                        Dim res = ProcessLiquidateRecovery(serv, 3, folio.RevenueControl.TopEventCopay, admission.NivelCoPayContribTop, admission.NivelCoPayContribPercentage, iNPACIENTTOPANU.PAGADOCOP, admission.NivelCoPayContribTopYear, LiquidationType, liquidate)
            '                        result.StateResult = res.StateResult
            '                        messageResult.Add(res.Message)
            '                        folio.RevenueControl.TopEventCopay += serv.SubTotalPatientSalesPrice
            '                    End If
            '                ElseIf recoveryFeeType = 2 Then 'Si es cuota moderadora
            '                    Dim res = ProcessLiquidateRecovery(serv, 2, folio.RevenueControl.TopEventFeeModerator, admission.NivelModeratorShareTop, admission.NivelModeratorSharePercentage, iNPACIENTTOPANU.PAGADOCMO, admission.NivelModeratorShareTopYear, LiquidationType, liquidate)
            '                    result.StateResult = res.StateResult
            '                    messageResult.Add(res.Message)
            '                    folio.RevenueControl.TopEventFeeModerator += serv.SubTotalPatientSalesPrice
            '                ElseIf recoveryFeeType = 1 Then 'El manual esta configurado en ninguno
            '                    serv.SubTotalPatientSalesPrice = 0 'Valor cero
            '                    serv.RecoveryFeeType = 1 'Ninguna
            '                    serv.ApplyRecoveryFee = 0 'No se puede cobrar
            '                    serv.PatientPercentage = 0 'Porcentaje aplicado
            '                    serv.ThirdPartySalesPrice = serv.GrandTotalSalesPrice - serv.SubTotalPatientSalesPrice 'Se calcula el valor de la entidad
            '                    serv.ThirdPartyPercentage = 100
            '                    result.StateResult = False
            '                    messageResult.Add(String.Format(ResourceManager.GetString("LiquidationMessage4", MODULE_NAME), serv.ServiceOrderDetail.IPSService.Code & "-" & serv.ServiceOrderDetail.IPSService.Name))
            '                Else 'No se cobra nada
            '                    serv.SubTotalPatientSalesPrice = 0 'Valor cero
            '                    serv.RecoveryFeeType = 1 'Ninguna
            '                    serv.ApplyRecoveryFee = 0 'No se puede cobrar
            '                    serv.PatientPercentage = 0 'Porcentaje aplicado
            '                    serv.ThirdPartySalesPrice = serv.GrandTotalSalesPrice - serv.SubTotalPatientSalesPrice 'Se calcula el valor de la entidad
            '                    serv.ThirdPartyPercentage = 100
            '                    result.StateResult = False
            '                    messageResult.Add(String.Format(ResourceManager.GetString("LiquidationMessage5", MODULE_NAME), serv.ServiceOrderDetail.RateManualDetail.RateManual.Code & "-" & serv.ServiceOrderDetail.RateManualDetail.RateManual.Name))
            '                End If
            '            End If
            '        Case 2 'Subsidiado
            '            Dim res = ProcessLiquidateRecovery(serv, 3, folio.RevenueControl.TopEventCopay, admission.NivelCoPaySubsibTop, admission.NivelCoPaySubsiPercentage, iNPACIENTTOPANU.PAGADOCOP, admission.NivelCoPayContribTopYear, LiquidationType, liquidate)
            '            result.StateResult = res.StateResult
            '            messageResult.Add(res.Message)
            '            folio.RevenueControl.TopEventCopay += serv.SubTotalPatientSalesPrice
            '        Case 3 'Vinculado
            '            Dim res = ProcessLiquidateRecovery(serv, 3, folio.RevenueControl.TopEventFeeRecovery, admission.NivelCoPayVincuTop, admission.NivelCoPayVincuPercentage, iNPACIENTTOPANU.PAGADOCRE, admission.NivelCoPayVincuTopYear, LiquidationType, liquidate)
            '            result.StateResult = res.StateResult
            '            messageResult.Add(res.Message)
            '            folio.RevenueControl.TopEventFeeRecovery += serv.SubTotalPatientSalesPrice
            '        Case Else 'Todo el resto queda en cero
            '            serv.SubTotalPatientSalesPrice = 0 'Valor cero
            '            serv.RecoveryFeeType = 1 'Ninguna
            '            serv.ApplyRecoveryFee = 0 'No se puede cobrar
            '            serv.PatientPercentage = 0 'Porcentaje aplicado
            '            serv.ThirdPartySalesPrice = serv.GrandTotalSalesPrice - serv.SubTotalPatientSalesPrice 'Se calcula el valor de la entidad
            '            serv.ThirdPartyPercentage = 100
            '            result.StateResult = False
            '            messageResult.Add(ResourceManager.GetString("LiquidationMessage7", MODULE_NAME))
            '    End Select
            '    totalStatus = totalStatus And result.StateResult
            '    If totalStatus = False Then
            '        If messageResult.Contains(ResourceManager.GetString("LiquidationMessage2", MODULE_NAME)) OrElse messageResult.Contains(ResourceManager.GetString("LiquidationMessage3", MODULE_NAME)) Then
            '            result.StateResult = True
            '            result.Message = String.Empty
            '        Else
            '            result.StateResult = False
            '            result.Message = String.Join(vbCrLf, messageResult.Distinct().ToArray)
            '        End If
            '        result.ObjectEmbbeded = folio
            '        Return result
            '    End If
            'Next
            'result.Message = messageResult.ToString()
            'result.ObjectEmbbeded = folio
            'result.StateResult = totalStatus
            ''Retornamos el resultado de la acción con el folio modificado cual sea las condiciones del ingreso y sus servicios
            'Return result
        Else
            Return Nothing
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene el objeto de comprobante contable
    ''' </summary>
    ''' <returns></returns>
    Public Function GenerateJournaVoucherLiquidation(folio As RevenueControlDetail, OperativeUnitId As Integer, invoiceNumber As String, reverse As Boolean) As ActionResult(Of JournalVouchers) Implements IBillingServices.GenerateJournaVoucherLiquidation
        Try
            Dim billinSettings As SettingsBilling = _settingsBillingRepository.GetSettingsBillingByIdUnitOperative(OperativeUnitId, False)
            If billinSettings Is Nothing OrElse billinSettings.Id = 0 Then
                Return New ActionResult(Of JournalVouchers) With {.StateResult = False, .Message = ResourceManager.GetString("SettingsParameterBillingNotFound")}
            End If

            Dim IdJournalVoucher As Integer = 0
            Dim journalVoucher As New JournalVouchers()
            Dim careGroup As CareGroup
            Dim cupsEntity As CUPSEntity
            Dim serviceOrderDetail As ServiceOrderDetail
            Dim journalVoucherDetailCR As JournalVoucherDetails
            Dim journalVoucherDetailDB As JournalVoucherDetails
            Dim journalVoucherDetailDiscount As JournalVoucherDetails
            Dim functionalUnit As Domain.Payroll.Entities.FunctionalUnit
            Dim mainAccounts As MainAccounts
            Dim sumDebit As Decimal = 0
            Dim sumCredit As Decimal = 0
            Dim settingsInventory As SettingInventory = _settingsInventoryRepository.GetSettingInventory(OperativeUnitId)
            If reverse Then
                IdJournalVoucher = billinSettings.InvoiceAnnulmentJournalVoucherTypeId
            Else
                IdJournalVoucher = billinSettings.InvoiceJournalVoucherTypeId
            End If
            Dim errorList As New List(Of String)
            With journalVoucher
                .IdJournalVoucher = IdJournalVoucher
                .VoucherDate = Date.Now
                .Status = 2
                .Detail = ""
                .EntityCode = invoiceNumber
                .EntityId = Nothing
                .EntityName = GetType(Invoice).Name
                .IsClosedYear = False
                For Each serviceOrderDetailDistribution As ServiceOrderDetailDistribution In folio.ServiceOrderDetailDistribution
                    serviceOrderDetail = serviceOrderDetailDistribution.ServiceOrderDetail
                    '_serviceOrderDetailRepository. _
                    'GetServiceOrderDetailByIdIncludes(serviceOrderDetailDistribution.ServiceOrderDetailId, {"ServiceOrderDetailSurgical", "CUPSEntity", "CUPSEntity.BillingConcept", "CUPSEntity.BillingConcept.BillingConceptAccount", "CareGroup", "ServiceOrderDetailSurgical.BillingConcept", "ServiceOrderDetailSurgical.BillingConcept.BillingConceptAccount"})
                    '********Detalle a debitar por el valor del servicio/producto con el descuento
                    journalVoucherDetailDB = New JournalVoucherDetails()
                    With journalVoucherDetailDB
                        'Es Servicio
                        careGroup = serviceOrderDetail.CareGroup
                        Dim careGroupAssign = _careGroupRepository.GetCareGroupByIdWithAssociations(careGroup.Id)
                        If careGroupAssign.ContractAccountingStructureId Is Nothing OrElse careGroupAssign.ContractAccountingStructure Is Nothing Then
                            Return New ActionResult(Of JournalVouchers) With {.StateResult = False, .Message = "El grupo de atención " + careGroupAssign.Code + " - " + careGroupAssign.Name + " no tiene asignado la estructura contable"}
                        End If
                        Select Case careGroup.CareGroupType
                            Case eCareGroupType.EAPBWithContract, eCareGroupType.EAPBWithOutContract, eCareGroupType.Aseguradora
                                .IdMainAccount = careGroupAssign.ContractAccountingStructure.AccountWithoutRadicateId
                            Case eCareGroupType.Particular
                                .IdMainAccount = careGroupAssign.ContractAccountingStructure.AccountParticularId
                        End Select
                        mainAccounts = GetMainAccounts(.IdMainAccount)
                        .IdThirdParty = IIf(mainAccounts.HandlesThirdParty, folio.ThirdPartyId, Nothing)
                        .IdCostCenter = IIf(mainAccounts.HandlesCostCenter, serviceOrderDetail.CostCenterId, Nothing)
                        If reverse Then
                            .CreditValue = serviceOrderDetailDistribution.GrandTotalSalesPrice
                            .DebitValue = 0
                            .Detail = "Anulación de Factura módulo de facturación"
                        Else
                            .CreditValue = 0
                            .DebitValue = serviceOrderDetailDistribution.GrandTotalSalesPrice
                            .Detail = "Creación de Factura módulo de facturación"
                        End If
                        sumCredit += .CreditValue
                        sumDebit += .DebitValue
                    End With
                    .JournalVoucherDetails.Add(journalVoucherDetailDB)

                    Dim discountValue As Decimal = 0
                    '*********Descuentos a tercero al Debito
                    If serviceOrderDetail.ThirdPartyDiscount > 0 Then
                        'Detalle a debitar por el valor del descuento del servicio/producto
                        journalVoucherDetailDiscount = New JournalVoucherDetails()
                        With journalVoucherDetailDiscount
                            If serviceOrderDetail.RecordType = 1 Then
                                'Es Servicio
                                cupsEntity = serviceOrderDetail.CUPSEntity 'GetCupsEntityByidIncludes(serviceOrderDetail.CUPSEntityId, Nothing)
                                'Dim bConcept As BillingConcept = GetBillingConcept(cupsEntity.BillingConceptId)
                                .IdMainAccount = cupsEntity.BillingConcept.DiscountAccountId
                            ElseIf serviceOrderDetail.RecordType = 2 Then
                                'Es Producto
                                .IdMainAccount = settingsInventory.DiscountSalesMainAccountId
                            End If
                            mainAccounts = GetMainAccounts(.IdMainAccount)
                            .IdThirdParty = IIf(mainAccounts.HandlesThirdParty, folio.ThirdPartyId, Nothing)
                            'El centro de costo se toma dependiendo del parametro del servicios IPS que dice (Obtiene centro de costo, ya sea unidad funcional o servicio IPS)
                            .IdCostCenter = IIf(mainAccounts.HandlesCostCenter, serviceOrderDetail.CostCenterId, Nothing)
                            If reverse Then
                                discountValue = serviceOrderDetailDistribution.GrandTotalDiscount
                                .CreditValue = discountValue
                                .DebitValue = 0
                                .Detail = "Anulación de Factura módulo de facturación"
                            Else
                                .CreditValue = 0
                                discountValue = serviceOrderDetailDistribution.GrandTotalDiscount
                                .DebitValue = discountValue 'serviceOrderDetail.InvoicedQuantity * serviceOrderDetail.ThirdPartyDiscount 'Cantidad Servicio * ValorDescuento
                                .Detail = "Creación de Factura módulo de facturación"
                            End If
                            sumCredit += .CreditValue
                            sumDebit += .DebitValue
                        End With
                        .JournalVoucherDetails.Add(journalVoucherDetailDiscount)
                    End If
                    '********Detalle a acreditar por el valor del servicio/producto + descuento
                    If serviceOrderDetail.RecordType = 1 AndAlso serviceOrderDetail.Presentation = 2 AndAlso billinSettings.AccountingForSurgical = 2 Then
                        'Servicios quirurgicos con parametros de facturacion con contabilizacion Detallada
                        Dim acomulateValue As Decimal = 0
                        For Each surgical In serviceOrderDetail.ServiceOrderDetailSurgical
                            journalVoucherDetailCR = New JournalVoucherDetails()
                            Dim bConcept As BillingConcept = surgical.BillingConcept
                            If bConcept.ConceptType = 1 Then
                                'Facturación básica
                                Return New ActionResult(Of JournalVouchers) With {.StateResult = False, .Message = String.Format("El concepto de facturación {0} no puede ser de tipo facturación básica", bConcept.Code)}
                            End If
                            If bConcept.AccountingType = 2 Then
                                'Cuenta por Tipo de Unidad Nota
                                Dim entity As Boolean = True
                                If careGroup.CareGroupType = eCareGroupType.Particular Then
                                    entity = False
                                End If
                                Dim unitType As Byte = IIf({1, 23}.Contains(serviceOrderDetail.FunctionalUnit.UnitType), 1,
                                                           IIf({2, 5, 6, 7, 8, 9, 10, 11, 16, 17, 18}.Contains(serviceOrderDetail.FunctionalUnit.UnitType), 2,
                                                               IIf({19}.Contains(serviceOrderDetail.FunctionalUnit.UnitType), 3, IIf({3, 4, 12, 13, 14, 15, 20, 21, 22, 24, 25}.Contains(serviceOrderDetail.FunctionalUnit.UnitType), 4, 0))))
                                If entity Then
                                    journalVoucherDetailCR.IdMainAccount = bConcept.BillingConceptAccount.Where(Function(o) o.UnitType = unitType).FirstOrDefault().EntityIncomeAccountId
                                Else
                                    journalVoucherDetailCR.IdMainAccount = bConcept.BillingConceptAccount.Where(Function(o) o.UnitType = unitType).FirstOrDefault().IndividualIncomeAccountId
                                End If
                            Else
                                Select Case careGroup.CareGroupType
                                    Case eCareGroupType.EAPBWithContract, eCareGroupType.EAPBWithOutContract, eCareGroupType.Aseguradora
                                        journalVoucherDetailCR.IdMainAccount = bConcept.EntityIncomeAccountId
                                    Case eCareGroupType.Particular
                                        journalVoucherDetailCR.IdMainAccount = bConcept.IndividualIncomeAccountId
                                End Select
                            End If
                            mainAccounts = GetMainAccounts(journalVoucherDetailCR.IdMainAccount)
                            journalVoucherDetailCR.IdThirdParty = IIf(mainAccounts.HandlesThirdParty, folio.ThirdPartyId, Nothing)
                            'El centro de costo se toma de serviceOrderDetail que se obtiene dependiendo del parametro del servicios IPS que dice (Obtiene centro de costo, ya sea unidad funcional o servicio IPS)
                            journalVoucherDetailCR.IdCostCenter = IIf(mainAccounts.HandlesCostCenter, serviceOrderDetail.CostCenterId, Nothing)
                            If reverse Then
                                If serviceOrderDetailDistribution.DistributionType = 2 Then
                                    'Esta distribuido
                                    Dim distribValue As Decimal = surgical.TotalSalesPrice * serviceOrderDetail.InvoicedQuantity * (serviceOrderDetailDistribution.ThirdPartySalesPrice / serviceOrderDetail.GrandTotalSalesPrice)
                                    acomulateValue += distribValue - Math.Truncate(distribValue)
                                    If surgical.Equals(serviceOrderDetail.ServiceOrderDetailSurgical(serviceOrderDetail.ServiceOrderDetailSurgical.Count - 1)) Then
                                        journalVoucherDetailCR.DebitValue += Math.Round(Math.Truncate(distribValue) + acomulateValue, 2)
                                    Else
                                        journalVoucherDetailCR.DebitValue = Math.Truncate(distribValue)
                                    End If
                                Else
                                    journalVoucherDetailCR.DebitValue = surgical.TotalSalesPrice * serviceOrderDetail.InvoicedQuantity
                                End If
                                journalVoucherDetailCR.CreditValue = 0
                                journalVoucherDetailCR.Detail = "Anulación de Factura módulo de facturación"
                            Else
                                If serviceOrderDetailDistribution.DistributionType = 2 Then
                                    'Esta distribuido
                                    Dim distribValue As Decimal = surgical.TotalSalesPrice * serviceOrderDetail.InvoicedQuantity * (serviceOrderDetailDistribution.ThirdPartySalesPrice / serviceOrderDetail.GrandTotalSalesPrice)
                                    acomulateValue += distribValue - Math.Truncate(distribValue)
                                    If surgical.Equals(serviceOrderDetail.ServiceOrderDetailSurgical(serviceOrderDetail.ServiceOrderDetailSurgical.Count - 1)) Then
                                        'Si es el ultimo de los surgical, se agrega el valor acomulado de los decimales para que sea exacto el valor
                                        journalVoucherDetailCR.CreditValue += Math.Round(Math.Truncate(distribValue) + acomulateValue, 2)
                                    Else
                                        journalVoucherDetailCR.CreditValue = Math.Truncate(distribValue)
                                    End If
                                Else
                                    journalVoucherDetailCR.CreditValue = surgical.TotalSalesPrice * serviceOrderDetail.InvoicedQuantity
                                End If
                                journalVoucherDetailCR.DebitValue = 0
                                journalVoucherDetailCR.Detail = "Creación de Factura módulo de facturación"
                            End If
                            sumCredit += journalVoucherDetailCR.CreditValue
                            sumDebit += journalVoucherDetailCR.DebitValue
                            .JournalVoucherDetails.Add(journalVoucherDetailCR)
                        Next
                        Continue For
                    End If
                    journalVoucherDetailCR = New JournalVoucherDetails()
                    With journalVoucherDetailCR
                        If serviceOrderDetail.RecordType = 1 Then
                            'Es Servicio
                            cupsEntity = serviceOrderDetail.CUPSEntity 'GetCupsEntityByidIncludes(serviceOrderDetail.CUPSEntityId, Nothing)
                            'Si el tipo de contabilizacion es 2 entonces miramos el tipo de unidad de la unidad funcional asociada al detalle de orden de servicio
                            'y luego se busca en los detalles de parametros de facturación y de allí se toman las cuentas
                            Dim bConcept As BillingConcept = cupsEntity.BillingConcept 'GetBillingConcept(cupsEntity.BillingConceptId)
                            If bConcept.AccountingType = 2 Then
                                'Cuenta por Tipo de Unidad Nota
                                Dim entity As Boolean = True
                                If careGroup.CareGroupType = eCareGroupType.Particular Then
                                    entity = False
                                End If
                                Dim unitType As Byte = IIf({1, 23}.Contains(serviceOrderDetail.FunctionalUnit.UnitType), 1,
                                                           IIf({2, 5, 6, 7, 8, 9, 10, 11, 16, 17, 18}.Contains(serviceOrderDetail.FunctionalUnit.UnitType), 2,
                                                               IIf({19}.Contains(serviceOrderDetail.FunctionalUnit.UnitType), 3, IIf({3, 4, 12, 13, 14, 15, 20, 21, 22, 24, 25}.Contains(serviceOrderDetail.FunctionalUnit.UnitType), 4, 0))))
                                If entity Then
                                    .IdMainAccount = bConcept.BillingConceptAccount.Where(Function(o) o.UnitType = unitType).FirstOrDefault().EntityIncomeAccountId
                                Else
                                    .IdMainAccount = bConcept.BillingConceptAccount.Where(Function(o) o.UnitType = unitType).FirstOrDefault().IndividualIncomeAccountId
                                End If
                            Else
                                Select Case careGroup.CareGroupType
                                    Case eCareGroupType.EAPBWithContract, eCareGroupType.EAPBWithOutContract, eCareGroupType.Aseguradora
                                        .IdMainAccount = bConcept.EntityIncomeAccountId
                                    Case eCareGroupType.Particular
                                        .IdMainAccount = bConcept.IndividualIncomeAccountId
                                End Select
                            End If
                        ElseIf serviceOrderDetail.RecordType = 2 Then
                            'Es Producto
                            Dim productGroup As ProductGroup = Nothing
                            If _dictionaryProductGroups.ContainsKey(CInt(serviceOrderDetail.ProductId)) Then
                                productGroup = _dictionaryProductGroups(CInt(serviceOrderDetail.ProductId))
                            Else
                                productGroup = _productGroupRepository.GetProductGroupByProductId(CInt(serviceOrderDetail.ProductId))
                                _dictionaryProductGroups.Add(CInt(serviceOrderDetail.ProductId), productGroup)
                            End If
                            If productGroup Is Nothing OrElse productGroup.Id = 0 Then
                                functionalUnit = _functionalUnitRepository.GetFunctionalUnitById(serviceOrderDetail.PerformsFunctionalUnitId)
                                errorList.Add(String.Format(ResourceManager.GetString("SettingParameterNotFountFunctionalUnit"), String.Concat(functionalUnit.Code, " - ", functionalUnit.Name)))
                            Else
                                .IdMainAccount = productGroup.IncomeAccountId
                            End If
                        End If
                        mainAccounts = GetMainAccounts(.IdMainAccount)
                        .IdThirdParty = IIf(mainAccounts.HandlesThirdParty, folio.ThirdPartyId, Nothing)
                        'El centro de costo se toma de serviceOrderDetail que se obtiene dependiendo del parametro del servicios IPS que dice (Obtiene centro de costo, ya sea unidad funcional o servicio IPS)
                        .IdCostCenter = IIf(mainAccounts.HandlesCostCenter, serviceOrderDetail.CostCenterId, Nothing)
                        If reverse Then
                            .CreditValue = 0
                            .DebitValue = serviceOrderDetailDistribution.GrandTotalSalesPrice + discountValue
                            .Detail = "Anulación de Factura módulo de facturación"
                        Else
                            .CreditValue = serviceOrderDetailDistribution.GrandTotalSalesPrice + discountValue 'serviceOrderDetail.InvoicedQuantity * serviceOrderDetail.SubTotalSalesPrice 'Total sin descuento
                            .DebitValue = 0
                            .Detail = "Creación de Factura módulo de facturación"
                        End If
                        sumCredit += .CreditValue
                        sumDebit += .DebitValue
                    End With
                    .JournalVoucherDetails.Add(journalVoucherDetailCR)
                Next
            End With
            If errorList.Count > 0 Then
                Return New ActionResult(Of JournalVouchers) With {.StateResult = False, .Message = String.Join(vbCrLf, errorList.Distinct().ToList())}
            End If

            Return New ActionResult(Of JournalVouchers) With {.StateResult = True, .ObjectEmbbeded = journalVoucher}
        Catch ex As Exception
            Return New ActionResult(Of JournalVouchers) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Carga el objeto de la factura a generar
    ''' </summary>
    ''' <returns></returns>
    Public Function CreateInvoiceObject(Folio As RevenueControlDetail, OperativeUnitId As Integer, settingBilling As SettingsBilling, billingAuthorization As BillingAuthorization, ListPortfolioAdvanceCrossing As List(Of PortfolioAdvance)) As ActionResult(Of Invoice) Implements IBillingServices.CreateInvoiceObject
        Dim careGroup As CareGroup = GetCareGroup(Folio.CareGroupId)
        Dim patient As INPACIENT = _patientRepository.GetPatientByIdentification(Folio.RevenueControl.PatientCode)
        Dim invoicePortfolioAdvance As InvoicePortfolioAdvance

        Dim invoice As New Invoice()
        With invoice
            .DocumentType = Folio.FolioType
            .InvoiceNumber = IIf(Folio.FolioType <> 5, String.Concat(billingAuthorization.InvoicePrefix, billingAuthorization.Consecutive), settingBilling.ConsecutiveControlCapitation.ToString())
            .RevenueControlDetailId = Folio.Id
            .AdmissionNumber = Folio.RevenueControl.AdmissionNumber
            .PatientCode = Folio.RevenueControl.PatientCode
            .CareGroupId = Folio.CareGroupId
            .InvoiceDate = Date.Now
            .HealthAdministratorId = Folio.HealthAdministratorId
            .ThirdPartyId = Folio.ThirdPartyId
            .InvoiceExpirationDate = Date.Now.AddDays(careGroup.InvoiceDeadlines)
            .TotalInvoice = Folio.TotalFolio
            .CapitationInitialDate = Nothing
            .CapitationEndDate = Nothing
            .CapitationlPatientsAmount = Nothing
            .CapitationPatientValue = 0 'Cuando el folio tiene control de capitación cuanto va aca?
            .ThirdPartySalesValue = Folio.TotalFolio - Folio.TotalPatientWithDiscount
            .ThirdPartyDiscountValue = Folio.ServiceOrderDetailDistribution.Sum(Function(o) o.GrandTotalDiscount) '(Sumatoria de los descuentos del tercero en service order detail DirectCast(FormOwner.GdcServices.DataSource, List(Of ViewListServiceOrderDetailXpo)).Sum(Function(x) x.GrandTotalDiscount))
            .TotalPatientSalesPrice = Folio.TotalPatientSalesPrice
            .PatientDiscount = Folio.PatientDiscount
            .PatientDiscountPercentage = Folio.PatientDiscountPercentage
            .TotalPatientWithDiscount = Folio.TotalPatientWithDiscount
            .CashReceiptId = Nothing 'PENDIENTE se agrega cuando se hace un cruce con un anticipo
            .JournalVoucherId = Nothing '(Establezco este id despues de generar el comprobante contable)
            .PatientPaidValue = ListPortfolioAdvanceCrossing.Sum(Function(o) o.CrossingValue) '0 'PENDIENTE (valor total del cruce)
            .ThirdPartyAccountReceivableValue = .ThirdPartySalesValue 'PENDIENTE
            .PatientAccountReceivableValue = .TotalPatientWithDiscount 'PENDIENTE
            '.ThirdPartyAccountReceivableId = Nothing
            .PatientAccountReceivableId = Nothing
            '.ReversalType = Nothing 'Para Anulación
            '.ReversalReason = Nothing 'Anulacion
            .PatientType = patient.IPTIPOPAC 'SACAR TABLA INPACIEN de Crystal
            .PatientAffiliatedType = patient.IPTIPOAFI 'SACAR TABLA INPACIEN de Crystal
            .PatientPaidAbility = patient.CAPACIPAG 'SACAR TABLA INPACIEN de Crystal
            .PatientSocialClass = patient.NIVECODIGO 'SACAR TABLA INPACIEN de Crystal  **************** ESPE CAMPO NO EXISTE NIVCODIGO en la tabla mapeada
            .CREETaxRetentionBaseValue = 0
            .CREETaxRetentionValue = 0
            .Status = 1 'Facturado
            .OperatingUnitId = OperativeUnitId
            For Each detail As ServiceOrderDetailDistribution In Folio.ServiceOrderDetailDistribution
                Dim invoiceDetail As New InvoiceDetail()
                With invoiceDetail
                    .ServiceOrderDetailId = detail.ServiceOrderDetailId
                    .GrandTotalSalesPrice = detail.GrandTotalSalesPrice
                    .GrandTotalDiscount = detail.GrandTotalDiscount
                    .DistributionType = detail.DistributionType
                    .ThirdPartySalesPrice = detail.ThirdPartySalesPrice
                    .ThirdPartyPercentage = detail.ThirdPartyPercentage
                    .ApplyRecoveryFee = detail.ApplyRecoveryFee
                    .RecoveryFeeType = detail.RecoveryFeeType
                    .SubTotalPatientSalesPrice = detail.SubTotalPatientSalesPrice
                    .PatientPercentage = detail.PatientPercentage
                End With
                .InvoiceDetail.Add(invoiceDetail)
            Next
            '
            If ListPortfolioAdvanceCrossing IsNot Nothing Then
                For Each portfolioAdvance As PortfolioAdvance In ListPortfolioAdvanceCrossing
                    invoicePortfolioAdvance = New InvoicePortfolioAdvance()
                    With invoicePortfolioAdvance
                        .PortfolioAdvanceId = portfolioAdvance.Id
                        .Value = portfolioAdvance.CrossingValue
                    End With
                    .InvoicePortfolioAdvance.Add(invoicePortfolioAdvance)
                Next
            End If
        End With
        Return New ActionResult(Of Invoice) With {.ObjectEmbbeded = invoice, .StateResult = True}
    End Function

#End Region



    ''' <summary>
    ''' Actualiza los valores del folio
    ''' </summary>
    ''' <param name="revenueControlDetailId">The revenue control detail identifier.</param>
    ''' <param name="folio"></param>
    ''' <returns></returns>
    Public Function UpdateRevenueControlDetailValues(revenueControlDetailId As Integer, Optional folio As RevenueControlDetail = Nothing, Optional operativeUnitId As Integer? = Nothing) As ActionResult Implements IBillingServices.UpdateRevenueControlDetailValues
        Dim errorList As New StringBuilder()
        Try
            If folio IsNot Nothing Then
                _revenueControlDetailRepository.SaveEntity(folio)
                _revenueControlDetailRepository.UnitWork.Commit()
            End If
            Dim result As SP_UpdateRevenueControlDetailValues_Result = _revenueControlDetailRepository.UpdateRevenueControlDetailValues(revenueControlDetailId, operativeUnitId)
            If Not result.StatusResult Then
                Return New ActionResult With {.StateResult = False, .Message = result.MessageResult}
            End If
            Return New ActionResult With {.StateResult = result.StatusResult, .Message = result.MessageResult}
        Catch ex As Exception
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Actualiza los valores del folio
    ''' </summary>
    ''' <param name="revenueControlDetailId">The revenue control detail identifier.</param>
    ''' <param name="folio"></param>
    ''' <returns></returns>
    Public Function UpdateRevenueControlDetailValuesNew(revenueControlDetailId As Integer, Optional folio As RevenueControlDetail = Nothing, Optional operativeUnitId As Integer? = Nothing) As ActionResult Implements IBillingServices.UpdateRevenueControlDetailValuesNew
        Dim errorList As New StringBuilder()
        Try
            _revenueControlDetailRepository.SaveEntity(folio)
            _revenueControlDetailRepository.UnitWork.Commit()
            Dim result As SP_UpdateRevenueControlDetailValuesNew_Result = _revenueControlDetailRepository.UpdateRevenueControlDetailValuesNew(revenueControlDetailId, operativeUnitId)
            If Not result.StatusResult Then
                Return New ActionResult With {.StateResult = False, .Message = result.MessageResult}
            End If
            Return New ActionResult With {.StateResult = result.StatusResult, .Message = result.MessageResult}
        Catch ex As Exception
            Return New ActionResult With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Gets the value service.
    ''' </summary>
    ''' <param name="CupsEntityId">The cups entity identifier.</param>
    ''' <param name="IPSServiceId">The ips service identifier.</param>
    ''' <param name="CareGroupId">The care group identifier.</param>
    ''' <param name="FunctionalUnitId">The functional unit identifier.</param>
    ''' <param name="Specialty">The specialty.</param>
    ''' <param name="ServiceDate">The service date.</param>
    ''' <param name="PatientGenus">The patient genus.</param>
    ''' <param name="PatientDateBirth">The patient date birth.</param>
    ''' <param name="InvoicedQuantity">The invoiced quantity.</param>
    ''' <param name="ProfessionalHealthCode">The professional health code.</param>
    ''' <param name="ProfessionalHealthThirdPartyId">The professional health third party identifier.</param>
    ''' <returns></returns>
    Public Function GetValueService(AdmissionNumber As String, CenterAttentionCode As String, CupsEntityId As Integer, IPSServiceId As Integer, CareGroupId As Integer, FunctionalUnitId As Integer?, Specialty As String, ServiceDate As Date, PatientGenus As Integer, PatientDateBirth As Date, InvoicedQuantity As Integer, ProfessionalHealthCode As String, ProfessionalHealthThirdPartyId As Integer?, Optional RiasId As Integer? = Nothing, Optional ContractDescriptionId As Integer? = Nothing) As ActionResult(Of ServiceOrderDetail) Implements IBillingServices.GetValueService
        Try
            Dim res As ActionResult(Of ServiceOrderDetail) = Me.GetServiceValue(AdmissionNumber, CenterAttentionCode, CupsEntityId, IPSServiceId, CareGroupId, FunctionalUnitId, Specialty, ServiceDate, PatientGenus, PatientDateBirth, InvoicedQuantity, ProfessionalHealthCode, ProfessionalHealthThirdPartyId, RiasId, ContractDescriptionId)
            If res.StateResult Then
                Dim res1 = RefactorServiceValue(res.ObjectEmbbeded)
                res.ObjectEmbbeded = res1.ObjectEmbbeded
                res.StateResult = res1.StateResult
                res.Message = res1.Message
                Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = res.StateResult, .ObjectEmbbeded = res.ObjectEmbbeded, .Message = res.Message, .MessageResult = res.MessageResult}
            Else
                Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = False, .Message = res.Message, .MessageResult = res.MessageResult}
            End If
        Catch ex As Exception
            Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    Private Sub setServiceValue(s As ServiceOrderDetailDistribution, servOrderDetailResult As ServiceOrderDetail, GuidHomologation As String, careGroupId As Integer) Implements IBillingServices.setServiceValue
        Dim round = 1
        If s.ServiceOrderDetail.RateManualId IsNot Nothing Then
            round = _rateManualRepository.GetRateManualById(s.ServiceOrderDetail.RateManualId).RoundService
        End If

        s.ServiceOrderDetail.StartTracking()
        s.ServiceOrderDetail.CareGroupId = careGroupId
        s.ServiceOrderDetail.CodeNameCareGroup = servOrderDetailResult.CodeNameCareGroup
        s.ServiceOrderDetail.CodeNameCostCenter = servOrderDetailResult.CodeNameCostCenter
        s.ServiceOrderDetail.CodeNameCups = servOrderDetailResult.CodeNameCups
        s.ServiceOrderDetail.CodeNameFunctionalUnit = servOrderDetailResult.CodeNameFunctionalUnit
        s.ServiceOrderDetail.CodeNameHealthAdministrator = servOrderDetailResult.CodeNameHealthAdministrator
        s.ServiceOrderDetail.CodeNameHealthProfessional = servOrderDetailResult.CodeNameHealthProfessional
        s.ServiceOrderDetail.CodeNameIpsService = servOrderDetailResult.CodeNameIpsService
        s.ServiceOrderDetail.CodeNameProduct = servOrderDetailResult.CodeNameProduct
        s.ServiceOrderDetail.CodeNameSpeciality = servOrderDetailResult.CodeNameSpeciality
        s.ServiceOrderDetail.RateManualSalePrice = servOrderDetailResult.RateManualSalePrice
        s.ServiceOrderDetail.IsSOAT = servOrderDetailResult.IsSOAT
        'VERIFICAR SI CUANDO ES UNO A UNO NO PASA NADA PORQUE ESTA LÍNEA SOLO APARECÍA CUANDO SE ENVIABAN LAS HOMOLOGACIONES
        s.ServiceOrderDetail.CodeAssociateService = GuidHomologation
        s.ApplyRecoveryFee = 1
        s.RecoveryFeeType = 1
        s.LastCaregroupId = careGroupId
        '*****cambiar el servicioIps
        s.ServiceOrderDetail.IPSServiceId = servOrderDetailResult.IPSServiceId
        '*****Agregar los detalles Quirurgicos
        If s.ServiceOrderDetail.Presentation = 2 Then
            'Quirurgico
            s.ServiceOrderDetail.ServiceOrderDetailSurgical.ToList().ForEach(Sub(o)
                                                                                 Dim ipsService As IPSService = _ipsServiceRepository.GetIPSServiceById(o.IPSServiceId)
                                                                                 Dim surgical1 As ServiceOrderDetailSurgical = Nothing
                                                                                 Select Case ipsService.ServiceClass
                                                                                     Case 1
                                                                                         surgical1 = servOrderDetailResult.ServiceOrderDetailSurgical.Where(Function(x) x.ClassServiceIps = "Ninguno").FirstOrDefault()
                                                                                     Case 2
                                                                                         surgical1 = servOrderDetailResult.ServiceOrderDetailSurgical.Where(Function(x) x.ClassServiceIps = "Cirujano").FirstOrDefault()
                                                                                     Case 3
                                                                                         surgical1 = servOrderDetailResult.ServiceOrderDetailSurgical.Where(Function(x) x.ClassServiceIps = "Anestesiólogo").FirstOrDefault()
                                                                                     Case 4
                                                                                         surgical1 = servOrderDetailResult.ServiceOrderDetailSurgical.Where(Function(x) x.ClassServiceIps = "Ayudante").FirstOrDefault()
                                                                                     Case 5
                                                                                         surgical1 = servOrderDetailResult.ServiceOrderDetailSurgical.Where(Function(x) x.ClassServiceIps = "Derecho Sala").FirstOrDefault()
                                                                                     Case 6
                                                                                         surgical1 = servOrderDetailResult.ServiceOrderDetailSurgical.Where(Function(x) x.ClassServiceIps = "Materiales Sutura").FirstOrDefault()
                                                                                     Case 7
                                                                                         surgical1 = servOrderDetailResult.ServiceOrderDetailSurgical.Where(Function(x) x.ClassServiceIps = "Instrumentación Quirúrgica").FirstOrDefault()
                                                                                 End Select
                                                                                 If surgical1 IsNot Nothing Then
                                                                                     surgical1.PerformsHealthProfessionalThirdPartyId = o.PerformsHealthProfessionalThirdPartyId
                                                                                     surgical1.PerformsHealthProfessionalCode = o.PerformsHealthProfessionalCode
                                                                                 End If
                                                                             End Sub)
            servOrderDetailResult.ServiceOrderDetailSurgical.ToList().ForEach(Sub(o)
                                                                                  s.ServiceOrderDetail.ServiceOrderDetailSurgical.Add(o)
                                                                              End Sub)
        End If
        Select Case s.ServiceOrderDetail.SettlementType
            Case eSettlementType.ManualDeTarifas
                s.ServiceOrderDetail.SubTotalSalesPrice = servOrderDetailResult.SubTotalSalesPrice
                s.ServiceOrderDetail.ThirdPartyDiscount = Math.Round((s.ServiceOrderDetail.SubTotalSalesPrice * (s.ServiceOrderDetail.ThirdPartyDiscountPercentage / 100)), 0)
                If (s.ServiceOrderDetail.SubTotalSalesPrice - s.ServiceOrderDetail.ThirdPartyDiscount) < 100 Then
                    s.ServiceOrderDetail.TotalSalesPrice = s.ServiceOrderDetail.SubTotalSalesPrice - s.ServiceOrderDetail.ThirdPartyDiscount
                Else
                    s.ServiceOrderDetail.TotalSalesPrice = Utils.RoundValue(s.ServiceOrderDetail.SubTotalSalesPrice - s.ServiceOrderDetail.ThirdPartyDiscount, round)
                End If
                If ((s.ServiceOrderDetail.SubTotalSalesPrice - s.ServiceOrderDetail.ThirdPartyDiscount) * s.Quantity) < 100 Then
                    s.ServiceOrderDetail.GrandTotalSalesPrice = Utils.RoundValue(s.ServiceOrderDetail.TotalSalesPrice * s.Quantity, 1)
                    s.ServiceOrderDetail.RoundService = 1
                Else
                    s.ServiceOrderDetail.GrandTotalSalesPrice = Utils.RoundValue(s.ServiceOrderDetail.TotalSalesPrice * s.Quantity, round)
                    s.ServiceOrderDetail.RoundService = round
                End If
                s.GrandTotalSalesPrice = s.ServiceOrderDetail.GrandTotalSalesPrice
                s.ThirdPartySalesPrice = s.ServiceOrderDetail.GrandTotalSalesPrice
                s.GrandTotalDiscount = s.ServiceOrderDetail.ThirdPartyDiscount * s.Quantity
                s.ThirdPartyPercentage = 100
                s.SubTotalPatientSalesPrice = 0
                s.PatientPercentage = 0
            Case eSettlementType.PorcentajeOtroServicio
                Dim sOrderDetail As ServiceOrderDetail = _serviceOrderDetailRepository.GetServiceOrderDetailById(s.ServiceOrderDetail.IncludeServiceOrderDetailId, False)
                s.ServiceOrderDetail.SubTotalSalesPrice = Math.Round(CDec(sOrderDetail.SubTotalSalesPrice * s.ServiceOrderDetail.RecoveryRatio / 100), 0)
                s.ServiceOrderDetail.ThirdPartyDiscount = Math.Round((s.ServiceOrderDetail.SubTotalSalesPrice * (s.ServiceOrderDetail.ThirdPartyDiscountPercentage / 100)), 0)
                If (s.ServiceOrderDetail.SubTotalSalesPrice - s.ServiceOrderDetail.ThirdPartyDiscount) < 100 Then
                    s.ServiceOrderDetail.TotalSalesPrice = s.ServiceOrderDetail.SubTotalSalesPrice - s.ServiceOrderDetail.ThirdPartyDiscount
                Else
                    s.ServiceOrderDetail.TotalSalesPrice = Utils.RoundValue(s.ServiceOrderDetail.SubTotalSalesPrice - s.ServiceOrderDetail.ThirdPartyDiscount, round)
                End If
                If ((s.ServiceOrderDetail.SubTotalSalesPrice - s.ServiceOrderDetail.ThirdPartyDiscount) * s.Quantity) < 100 Then
                    s.ServiceOrderDetail.GrandTotalSalesPrice = Utils.RoundValue(s.ServiceOrderDetail.TotalSalesPrice * s.Quantity, 1)
                    s.ServiceOrderDetail.RoundService = 1
                Else
                    s.ServiceOrderDetail.GrandTotalSalesPrice = Utils.RoundValue(s.ServiceOrderDetail.TotalSalesPrice * s.Quantity, round)
                    s.ServiceOrderDetail.RoundService = round
                End If
                s.GrandTotalSalesPrice = s.ServiceOrderDetail.GrandTotalSalesPrice
                s.ThirdPartySalesPrice = s.ServiceOrderDetail.GrandTotalSalesPrice
                s.GrandTotalDiscount = s.ServiceOrderDetail.ThirdPartyDiscount * s.Quantity
                s.ThirdPartyPercentage = 100
                s.SubTotalPatientSalesPrice = 0
                s.PatientPercentage = 0
            Case eSettlementType.PorcentajeMismoServicio
                Dim sOrderDetail As ServiceOrderDetail = servOrderDetailResult
                s.ServiceOrderDetail.SubTotalSalesPrice = Math.Round(CDec(sOrderDetail.SubTotalSalesPrice * s.ServiceOrderDetail.RecoveryRatio / 100), 0)
                s.ServiceOrderDetail.ThirdPartyDiscount = Math.Round((s.ServiceOrderDetail.SubTotalSalesPrice * (s.ServiceOrderDetail.ThirdPartyDiscountPercentage / 100)), 0)
                If (s.ServiceOrderDetail.SubTotalSalesPrice - s.ServiceOrderDetail.ThirdPartyDiscount) < 100 Then
                    s.ServiceOrderDetail.TotalSalesPrice = s.ServiceOrderDetail.SubTotalSalesPrice - s.ServiceOrderDetail.ThirdPartyDiscount
                Else
                    s.ServiceOrderDetail.TotalSalesPrice = Utils.RoundValue(s.ServiceOrderDetail.SubTotalSalesPrice - s.ServiceOrderDetail.ThirdPartyDiscount, round)
                End If
                If ((s.ServiceOrderDetail.SubTotalSalesPrice - s.ServiceOrderDetail.ThirdPartyDiscount) * s.Quantity) < 100 Then
                    s.ServiceOrderDetail.GrandTotalSalesPrice = Utils.RoundValue(s.ServiceOrderDetail.TotalSalesPrice * s.Quantity, 1)
                    s.ServiceOrderDetail.RoundService = 1
                Else
                    s.ServiceOrderDetail.GrandTotalSalesPrice = Utils.RoundValue(s.ServiceOrderDetail.TotalSalesPrice * s.Quantity, round)
                    s.ServiceOrderDetail.RoundService = round
                End If
                s.GrandTotalSalesPrice = s.ServiceOrderDetail.GrandTotalSalesPrice
                s.ThirdPartySalesPrice = s.ServiceOrderDetail.GrandTotalSalesPrice
                s.GrandTotalDiscount = s.ServiceOrderDetail.ThirdPartyDiscount * s.Quantity
                s.ThirdPartyPercentage = 100
                s.SubTotalPatientSalesPrice = 0
                s.PatientPercentage = 0
            Case eSettlementType.IncluidoOtroServicio
                'No se recalcula
                With s.ServiceOrderDetail
                    .SubTotalSalesPrice = 0
                    .ThirdPartyDiscount = 0
                    .ThirdPartyDiscountPercentage = 0
                    .TotalSalesPrice = 0
                    .GrandTotalSalesPrice = 0
                End With
                With s
                    .GrandTotalSalesPrice = 0
                    .GrandTotalDiscount = 0
                    .ThirdPartySalesPrice = 0
                End With
                'Si es quirurgico en los detalles quirurgicos se establecen a cero los valores
                Dim serviceOrderDetailSurgical = s.ServiceOrderDetail.ServiceOrderDetailSurgical
                For Each surgical In serviceOrderDetailSurgical
                    surgical.TotalSalesPrice = 0
                Next

        End Select

        s.StartTracking()
        s.MarkAsModified()
    End Sub

    ''' <summary>
    ''' Gets the value.
    ''' </summary>
    ''' <param name="serviceOrderDetail">The service order detail.</param>
    ''' <returns></returns>
    Public Function RefactorServiceValue(serviceOrderDetail As ServiceOrderDetail) As ActionResult(Of ServiceOrderDetail)
        If serviceOrderDetail.Presentation <> 2 Then
            Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = True, .ObjectEmbbeded = serviceOrderDetail}
        End If
        With serviceOrderDetail
            Dim hasMaterial = .ServiceOrderDetailSurgical.Any(Function(x) x.ClassServiceIps = "Materiales Sutura")
            Dim listSurgicalProcedureService = _surgicalProcedureServiceRepository.GetSurgicalProcedureServiceByIPSServiceId(serviceOrderDetail.IPSServiceId)
            Dim listSurgicalDefault = listSurgicalProcedureService.FindAll(Function(x) x.DefaultService = True)

            For i As Integer = 0 To listSurgicalDefault.Count - 1 Step 1
                'si el item es derecho a sala busco el ips para materiales de sutura
                If listSurgicalDefault(i).ClassService = ResourceManager.GetString("RightRoom", "Contract") And Not hasMaterial Then
                    If .SurgicalInterventionType Is Nothing Or .SurgicalInterventionType < 9 Then
                        Dim res = GetIPSSutureMaterials(listSurgicalDefault(i), listSurgicalProcedureService, serviceOrderDetail)
                        If Not res.StateResult Then
                            Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = False, .Message = res.Message}
                        End If
                        .ServiceOrderDetailSurgical.ElementAt(.ServiceOrderDetailSurgical.Count - 1).IncomeMainAccountId = .IncomeMainAccountId
                    Else
                        'si es cruento consulto el manual de tarifas para obtener el ips de materiales
                        Dim rateManual As RateManual
                        rateManual = _rateManualRepository.GetRateManualById(.RateManualId)
                        Dim ipsSutureMaterials As IPSService
                        ipsSutureMaterials = _ipsServiceRepository.GetIPSServiceById(rateManual.MaterialNoBloodyIPSServiceId)
                        Dim procedureServiceTpm As New SurgicalProcedureService
                        With procedureServiceTpm
                            .IPSServiceParentId = serviceOrderDetail.IPSServiceId
                            .IPSServiceId = ipsSutureMaterials.Id
                            .ServiceAmount = 1
                            .DefaultService = True
                            .ValueItemServiceOrderDetail = 0
                            .CodeNameService = ipsSutureMaterials.Code + " - " + ipsSutureMaterials.Name
                            .ClassService = ResourceManager.GetString("SutureMaterials", "Contract")
                        End With
                        listSurgicalProcedureService.Add(procedureServiceTpm)
                        'listSurgicalDefault.Add(procedureServiceTpm)
                        listSurgicalDefault(i).ValueItemServiceOrderDetail = .ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalDefault(i).IPSServiceId).FirstOrDefault().TotalSalesPrice
                        listSurgicalDefault(i).PerformsHealthProfessionalCode = .ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalDefault(i).IPSServiceId).FirstOrDefault().PerformsHealthProfessionalCode
                        'asigno valores al item materiales de sutura 
                        Dim detailSuturematerial = listSurgicalProcedureService.Find(Function(x) x.ClassService = ResourceManager.GetString("SutureMaterials", "Contract"))
                        detailSuturematerial.ValueItemServiceOrderDetail = .ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = ipsSutureMaterials.Id).FirstOrDefault().TotalSalesPrice
                        detailSuturematerial.PerformsHealthProfessionalCode = .ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = ipsSutureMaterials.Id).FirstOrDefault().PerformsHealthProfessionalCode

                    End If
                Else
                    listSurgicalDefault(i).ValueItemServiceOrderDetail = .ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalDefault(i).IPSServiceId).FirstOrDefault().TotalSalesPrice
                    listSurgicalDefault(i).PerformsHealthProfessionalCode = .ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalDefault(i).IPSServiceId).FirstOrDefault().PerformsHealthProfessionalCode
                End If
            Next
        End With
        Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = True, .ObjectEmbbeded = serviceOrderDetail}
    End Function

    Private Function GetIPSSutureMaterials(surgicalProcedureService As SurgicalProcedureService, listSurgicalProcedureService As List(Of SurgicalProcedureService), serviceOrderDetail As ServiceOrderDetail) As ActionResult
        'Using modelIpsService As New MIPSService(Me.Tag)
        Dim ipsRightRoom = _ipsServiceRepository.GetIPSServiceById(surgicalProcedureService.IPSServiceId)
        If ipsRightRoom.AssociatedMaterialIPSServiceId IsNot Nothing Then
            'consulto el ips para el material de sutura
            Dim ipsSutureMaterials = _ipsServiceRepository.GetIPSServiceById(ipsRightRoom.AssociatedMaterialIPSServiceId)
            Dim procedureServiceTpm As New SurgicalProcedureService
            With procedureServiceTpm
                .IPSServiceParentId = surgicalProcedureService.IPSServiceParentId
                .IPSServiceId = ipsSutureMaterials.Id
                .ServiceAmount = 1
                .DefaultService = True
                .ValueItemServiceOrderDetail = 0
                .CodeNameService = ipsSutureMaterials.Code + " - " + ipsSutureMaterials.Name
                .ClassService = ResourceManager.GetString("SutureMaterials", "Contract")
            End With
            listSurgicalProcedureService.Add(procedureServiceTpm)
        End If
        'Dim indexItem = listServiceOrderDetailPopup.IndexOf(ServiceOrderDetail)
        'listServiceOrderDetailPopup.Remove(ServiceOrderDetail)
        'Using model As New MServiceOrder(Me.Tag)
        listSurgicalProcedureService.ForEach(Sub(x) x.ValueItemServiceOrderDetail = 0)
        listSurgicalProcedureService.ForEach(Sub(x) x.PerformsHealthProfessionalCode = Nothing)
        'obetengo todos los valores por defecto 
        Dim listSurgicalProcedureDefault = listSurgicalProcedureService.FindAll(Function(x) x.DefaultService = True)
        'consulto el valor enviando como parametro el listado de los valores por defecto y el detalle que se esta trabajando
        Dim result = GetServiceValueBySurgicalProcedureService(serviceOrderDetail, listSurgicalProcedureDefault)
        If result.StateResult = True Then
            serviceOrderDetail = result.ObjectEmbbeded
            'asigno los nuevos valores al listado del detalle quirurgico
            For i As Integer = 0 To listSurgicalProcedureDefault.Count - 1 Step 1
                listSurgicalProcedureDefault(i).ValueItemServiceOrderDetail = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalProcedureDefault(i).IPSServiceId).FirstOrDefault().TotalSalesPrice
                listSurgicalProcedureDefault(i).PerformsHealthProfessionalCode = serviceOrderDetail.ServiceOrderDetailSurgical.Where(Function(x) x.IPSServiceId = listSurgicalProcedureDefault(i).IPSServiceId).FirstOrDefault().PerformsHealthProfessionalCode
            Next
        Else
            Return New ActionResult With {.StateResult = False, .Message = result.Message}
        End If
        'End Using
        'inserto el item en la misma posicion qe estaba antes de ser eliminado
        'listServiceOrderDetailPopup.Insert(indexItem, ServiceOrderDetail)
        'End Using
        Return New ActionResult With {.StateResult = True}
    End Function

    ''' <summary>
    ''' Crea un nuevo detalle de orden de servicio para distribucion por unidades
    ''' </summary>
    ''' <param name="serviceOrderDetail">The service order detail.</param>
    ''' <returns></returns>
    Public Function CreateNewServiceOrderDetail(serviceOrderDetail As ServiceOrderDetail, invoiceQuantity As Integer) As ServiceOrderDetail Implements IBillingServices.CreateNewServiceOrderDetail
        Dim sod As New ServiceOrderDetail()
        With sod
            .ServiceOrderId = serviceOrderDetail.ServiceOrderId
            .CareGroupId = serviceOrderDetail.CareGroupId
            .HealthAdministratorId = serviceOrderDetail.HealthAdministratorId
            .ThirdPartyId = serviceOrderDetail.ThirdPartyId
            .RecordType = serviceOrderDetail.RecordType
            .CUPSEntityId = serviceOrderDetail.CUPSEntityId
            .IPSServiceId = serviceOrderDetail.IPSServiceId
            .HospitalStayId = serviceOrderDetail.HospitalStayId
            .HospitalStayDetailId = serviceOrderDetail.HospitalStayDetailId
            .CUPSAssociateService = serviceOrderDetail.CUPSAssociateService
            .CodeAssociateService = serviceOrderDetail.CodeAssociateService
            .IsPackage = serviceOrderDetail.IsPackage
            .Packaging = serviceOrderDetail.Packaging
            .PackageServiceOrderDetailId = serviceOrderDetail.PackageServiceOrderDetailId
            .LiquidationType = serviceOrderDetail.LiquidationType
            .Presentation = serviceOrderDetail.Presentation
            .ProductId = serviceOrderDetail.ProductId
            .InvoicedQuantity = invoiceQuantity
            .SupplyQuantity = serviceOrderDetail.SupplyQuantity
            .DevolutionQuantity = serviceOrderDetail.DevolutionQuantity
            .RateManualSalePrice = serviceOrderDetail.RateManualSalePrice
            .CostValue = serviceOrderDetail.CostValue
            .ServiceDate = serviceOrderDetail.ServiceDate
            .AuthorizationNumber = serviceOrderDetail.AuthorizationNumber
            .PerformsFunctionalUnitId = serviceOrderDetail.PerformsFunctionalUnitId
            .PerformsHealthProfessionalCode = serviceOrderDetail.PerformsHealthProfessionalCode
            .PerformsHealthProfessionalThirdPartyId = serviceOrderDetail.PerformsHealthProfessionalThirdPartyId
            .BillingConceptId = serviceOrderDetail.BillingConceptId
            .CostCenterId = serviceOrderDetail.CostCenterId
            .SettlementType = serviceOrderDetail.SettlementType
            .IncludeServiceOrderDetailId = serviceOrderDetail.IncludeServiceOrderDetailId
            .RecoveryRatio = serviceOrderDetail.RecoveryRatio
            .RateManualId = serviceOrderDetail.RateManualId
            .RateManualType = serviceOrderDetail.RateManualType
            .RateManualDetailId = serviceOrderDetail.RateManualDetailId
            '.RateByFixedId = serviceOrderDetail.RateByFixedId
            '.CareGroupRateId = serviceOrderDetail.CareGroupRateId
            .SubTotalSalesPrice = serviceOrderDetail.SubTotalSalesPrice
            .ThirdPartyDiscount = serviceOrderDetail.ThirdPartyDiscount
            .ThirdPartyDiscountPercentage = serviceOrderDetail.ThirdPartyDiscountPercentage
            .TotalSalesPrice = serviceOrderDetail.TotalSalesPrice
            .GrandTotalSalesPrice = serviceOrderDetail.TotalSalesPrice * .InvoicedQuantity
            .SurchargeApply = serviceOrderDetail.SurchargeApply
            .SurgicalInterventionType = serviceOrderDetail.SurgicalInterventionType
            .SurgeryNumber = serviceOrderDetail.SurgeryNumber
            .IsFirstEvent = serviceOrderDetail.IsFirstEvent
            .IncomeMainAccountId = serviceOrderDetail.IncomeMainAccountId
            .RoundService = 1
        End With
        Return sod
    End Function

    Public Function GetServiceValueByManualId(ServiceDetail As ServiceOrderDetail, IPSServiceId As Integer, CareGroupId As Integer, FunctionalUnitId As Integer?, Specialty As String, ServiceDate As Date, PatientGenus As Integer, PatientDateBirth As Date, InvoicedQuantity As Integer, ProfessionalHealthCode As String, ProfessionalHealthThirdPartyId As Integer?) As ActionResult(Of ServiceOrderDetail) Implements IBillingServices.GetServiceValueByManualId
        Dim IpsService As IPSService
        Dim serviceOrderDetail As ServiceOrderDetail = New ServiceOrderDetail
        Dim FlagTaxInclude As Boolean = _companySettingsRepository.FirstOrDefault(Function(x) x.Id > 0)?.SalePriceIncludeTax
        serviceOrderDetail.StartTracking()

        'si el parametro ObtainCostCenter = 1 obtengo el centro de costo de la unidad funcional, si es 2 se obtiene del grupo de servicio IPS
        Dim costCenterFunctionalunit As Integer = 0
        If FunctionalUnitId IsNot Nothing AndAlso FunctionalUnitId > 0 Then
            Dim functionalUnit As Domain.Payroll.Entities.FunctionalUnit = _functionalUnitRepository.GetFunctionalUnitById(FunctionalUnitId)
            costCenterFunctionalunit = functionalUnit.CostCenterId
            serviceOrderDetail.CodeNameFunctionalUnit = functionalUnit.Code + " - " + functionalUnit.Name
        End If
        Dim idCostCenter As Integer = costCenterFunctionalunit
        Dim costCenter As Domain.Payroll.Entities.CostCenter = Nothing
        costCenter = _costCenterRepository.GetCostCenterById(idCostCenter, False)
        serviceOrderDetail.CostCenterId = costCenter.Id
        serviceOrderDetail.CodeNameCostCenter = costCenter.Code + " - " + costCenter.Name

        Dim errorIps As New StringBuilder
        IpsService = _ipsServiceRepository.GetIPSServiceById(IPSServiceId)

        serviceOrderDetail.ServiceType = IpsService.ServiceType
        'valido que el servicio se pueda cobrar a hombre o mujer
        If PatientGenus = 1 And IpsService.InMale = False Then
            errorIps.AppendLine(String.Format(ResourceManager.GetString("IPSFemale", MODULE_NAME), IpsService.Code))
        End If
        If PatientGenus = 2 And IpsService.InFemale = False Then
            errorIps.AppendLine(String.Format(ResourceManager.GetString("IPSMale", MODULE_NAME), IpsService.Code))
        End If

        'valido la edad del paciente para poder cobrar el servicio dependiendo de los rango que trae el servicio ips
        Dim agePatient = DateAndTime.DateDiff(DateInterval.Day, PatientDateBirth, DateTime.Now)
        Dim minimunAgeIPS As Integer = 0
        Dim maximunAgeIPS As Integer = 0
        Select Case IpsService.MinimunAgeUnit
            Case 1 'años 
                minimunAgeIPS = IpsService.MinimunAge * 360
            Case 2 'meses
                minimunAgeIPS = IpsService.MinimunAge * 30
            Case 3 'dias
                minimunAgeIPS = IpsService.MinimunAge
        End Select
        Select Case IpsService.MaximumAgeUnit
            Case 1 'años
                maximunAgeIPS = IpsService.MaximumAge * 360
            Case 2 'meses
                maximunAgeIPS = IpsService.MaximumAge * 30
            Case 3 'dias
                maximunAgeIPS = IpsService.MaximumAge
        End Select
        If agePatient < minimunAgeIPS Then
            errorIps.AppendLine(String.Format(ResourceManager.GetString("MinimumAge", MODULE_NAME), IpsService.Code))
        End If
        If agePatient > maximunAgeIPS Then
            errorIps.AppendLine(String.Format(ResourceManager.GetString("MaximumAge", MODULE_NAME), IpsService.Code))
        End If
        If errorIps.Length > 0 Then
            Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = False, .Message = errorIps.ToString()}
        End If

        serviceOrderDetail.CodeNameIpsService = IpsService.Code + " - " + IpsService.Name
        'serviceOrderDetail.CodeNameCups = cupsEntity.Code + " - " + cupsEntity.Description
        'Consulto la definicion de la tarifa
        Dim rateVariation As Decimal = 0 'Solo se llena si la tarifa es estandar
        Dim rateManual As RateManual = Nothing 'Solo se llena si la tarifa es estandar
        Dim liquidationType As Integer = 0
        Dim manualType As Integer = 0 'Solo se llena si la tarifa es fija
        Dim salesValue As Decimal = 0 'Solo se llena si la tarifa es fija
        Dim salesValueWithSurcharge As Decimal = 0 'Solo se llena si la tarifa es fija

        If ServiceDetail.DefinitionRateDetailConditionId IsNot Nothing Then 'Fue por que la tarifa se saco de una condicion
            'asigno la bandera para saber si en presentacion puedo cambiar el valor del servicio o no
            serviceOrderDetail.AllowValueChange = ServiceDetail.AllowValueChange
            serviceOrderDetail.DefinitionRateDetailConditionId = ServiceDetail.DefinitionRateDetailConditionId
            serviceOrderDetail.LiquidationType = ServiceDetail.LiquidationType
            Dim conditionRate = _definitionRateDetailConditionRepository.GetDefinitionRateDetailConditionById(ServiceDetail.DefinitionRateDetailConditionId.Value, False)
            rateVariation = conditionRate.RateVariation
            If conditionRate.LiquidationType = 3 Then
                Dim rateManualValidity = _rateManualValidityRepository.GetRateManualValidityByIdWithAggregates(conditionRate.RateManualValidityId)
                Dim rateManualValidityDetail = rateManualValidity.RateManualValidityDetail.Where(Function(d) d.InitialDate <= ServiceDate AndAlso d.EndDate >= ServiceDate).FirstOrDefault
                If rateManualValidityDetail Is Nothing Then
                    Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = False, .Message = String.Format("El servicio IPS {0} no tiene parametrizado un manual de tarifas para la vigencia {1} - {2} en la fecha del servicio {3}", serviceOrderDetail.CodeNameIpsService, rateManualValidity.Code, rateManualValidity.Name, serviceOrderDetail.ServiceDate.ToString("yyyy-mm-dd"))}
                End If
                rateManual = _rateManualRepository.GetRateManualById(rateManualValidityDetail.RateManualId)
            Else
                rateManual = _rateManualRepository.GetRateManualById(conditionRate.RateManualId)
            End If
        Else 'Es por que la condicion fue ninguna y la tarifa esta en la cabecera
            'asigno la bandera para saber si en presentacion puedo cambiar el valor del servicio o no
            serviceOrderDetail.AllowValueChange = ServiceDetail.AllowValueChange
            serviceOrderDetail.DefinitionRateDetailId = ServiceDetail.DefinitionRateDetailId
            Dim detailRate = _definitionRateDetailRepository.GetDefinitionRateDetailById(ServiceDetail.DefinitionRateDetailId.Value, False)
            rateVariation = detailRate.RateVariation
            If detailRate.LiquidationType = 3 Then
                Dim rateManualValidity = _rateManualValidityRepository.GetRateManualValidityByIdWithAggregates(detailRate.RateManualValidityId)
                Dim rateManualValidityDetail = rateManualValidity.RateManualValidityDetail.Where(Function(d) d.InitialDate <= ServiceDate AndAlso d.EndDate >= ServiceDate).FirstOrDefault
                If rateManualValidityDetail Is Nothing Then
                    Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = False, .Message = String.Format("El servicio IPS {0} no tiene parametrizado un manual de tarifas para la vigencia {1} - {2} en la fecha del servicio {3}", serviceOrderDetail.CodeNameIpsService, rateManualValidity.Code, rateManualValidity.Name, serviceOrderDetail.ServiceDate.ToString("yyyy-mm-dd"))}
                End If
                rateManual = _rateManualRepository.GetRateManualById(rateManualValidityDetail.RateManualId)
            Else
                rateManual = _rateManualRepository.GetRateManualById(detailRate.RateManualId)
            End If
        End If

        serviceOrderDetail.Presentation = IpsService.Presentation
        If IpsService.Presentation = 2 Then
            'si el servicio ips es quirurgico
            'obtengo los valores por defecto del ips quirurgico
            Dim listDetailSurgicalDefault = _surgicalProcedureServiceRepository.GetSurgicalProcedureServiceByIPSServiceId(IpsService.Id).FindAll(Function(x) x.DefaultService = True)
            Dim errorsSurgical As New StringBuilder
            Dim errorTotalSurgical = New StringBuilder()
            For Each itemSurgical In listDetailSurgicalDefault
                'consulto el manual de tarifas quirurgico
                Dim rateManualDetailSurgical As RateManualDetailSurgical = Nothing
                Dim ipsTmp = _ipsServiceRepository.GetIPSServiceById(itemSurgical.IPSServiceId)
                Dim round As Integer = 1
                Dim costValueItem = 0
                'valido el tipo del manual de tarifas - Tipo del manual tarifario 1 - ISS 2001 , 2 - ISS 2004,  3 - SOAT
                If rateManual.Type < 3 Then
                    If ipsTmp.ServiceClass = 2 Or ipsTmp.ServiceClass = 3 Or ipsTmp.ServiceClass = 4 Then ' 2 - Cirujano,  3 - Anesteciologo, 4 - Ayudante
                        If IpsService.UVRNumber > UVRNUMBER AndAlso ipsTmp.ApplyChangeScore Then
                            costValueItem = IpsService.UVRNumber * ipsTmp.NewScore * itemSurgical.ServiceAmount
                        Else
                            costValueItem = IpsService.UVRNumber * ipsTmp.Score * itemSurgical.ServiceAmount
                        End If
                    Else
                        If ipsTmp.ServiceClass = 5 AndAlso IpsService.UVRNumber > UVRNUMBER AndAlso ipsTmp.ApplyChangeScore Then '5 - Derecho Sala
                            costValueItem = IpsService.UVRNumber * ipsTmp.NewScore * itemSurgical.ServiceAmount
                        Else
                            rateManualDetailSurgical = _rateManualDetailSurgicalRepository.GetSurgicalDetailServiceOrder(rateManual.Id, itemSurgical.IPSServiceId, IpsService.SurgicalGroupId, IpsService.UVRNumber, IpsService.ServiceManual)
                            If rateManualDetailSurgical Is Nothing Then
                                'valido que el ips este parametrizado en el manual de tarifas
                                errorsSurgical.AppendLine(String.Format(ResourceManager.GetString("NotParameterizedIPSSurgical", MODULE_NAME), ipsTmp.Code + " - " + ipsTmp.Name))
                                Continue For
                            End If
                            costValueItem = rateManualDetailSurgical.SalesValue * itemSurgical.ServiceAmount
                        End If
                    End If
                Else
                    rateManualDetailSurgical = _rateManualDetailSurgicalRepository.GetSurgicalDetailServiceOrder(rateManual.Id, itemSurgical.IPSServiceId, IpsService.SurgicalGroupId, IpsService.UVRNumber, IpsService.ServiceManual)
                    If rateManualDetailSurgical Is Nothing Then
                        'valido que el ips este parametrizado en el manual de tarifas
                        errorsSurgical.AppendLine(String.Format(ResourceManager.GetString("NotParameterizedIPSSurgical", MODULE_NAME), ipsTmp.Code + " - " + ipsTmp.Name))
                        Continue For
                    End If
                    costValueItem = rateManualDetailSurgical.SalesValue * itemSurgical.ServiceAmount
                    serviceOrderDetail.IsSOAT = True
                End If

                Dim totalSalesPriceItem = costValueItem + (costValueItem * (rateVariation / 100))
                If totalSalesPriceItem > rateManual.RoundService Then
                    totalSalesPriceItem = Utils.RoundValue(totalSalesPriceItem, rateManual.RoundService)
                    round = rateManual.RoundService
                End If

                serviceOrderDetail.RateManualType = rateManual.Type
                serviceOrderDetail.RateManualId = rateManual.Id
                serviceOrderDetail.SubTotalSalesPrice += totalSalesPriceItem
                serviceOrderDetail.RateManualSalePrice += totalSalesPriceItem
                serviceOrderDetail.TotalSalesPrice += totalSalesPriceItem
                serviceOrderDetail.GrossValue += totalSalesPriceItem
                serviceOrderDetail.PerformsHealthProfessionalThirdPartyId = ProfessionalHealthThirdPartyId

                serviceOrderDetail.GrandTotalSalesPrice = serviceOrderDetail.TotalSalesPrice * InvoicedQuantity
                serviceOrderDetail.RoundService = 1
                'si el valor total es mayor al redondeo entonces se aplica
                If serviceOrderDetail.GrandTotalSalesPrice > rateManual.RoundService Then
                    serviceOrderDetail.GrandTotalSalesPrice = Utils.RoundValue(serviceOrderDetail.GrandTotalSalesPrice, rateManual.RoundService)
                    serviceOrderDetail.RoundService = rateManual.RoundService
                End If

                Dim serviceOrderDetailSurgical As New ServiceOrderDetailSurgical
                serviceOrderDetailSurgical.StartTracking()
                'asigno los valores a la entidad serviceOrderDetailSurgical que es donde se almacenan los valores de los detalles quirurgicos con valor por defecto
                With serviceOrderDetailSurgical
                    .CodeNameIpsService = ipsTmp.Code + " - " + ipsTmp.Name
                    .IPSServiceId = itemSurgical.IPSServiceId
                    .InvoicedQuantity = itemSurgical.ServiceAmount
                    .LiquidationPercentage = 0
                    .TotalSalesPrice = totalSalesPriceItem
                    .ClassServiceIps = itemSurgical.ClassService
                    .RateManualSalePrice = totalSalesPriceItem
                    If itemSurgical.ClassService = ResourceManager.GetString("Surgeon", "Contract") Then
                        .PerformsHealthProfessionalCode = ProfessionalHealthCode
                        .PerformsHealthProfessionalThirdPartyId = ProfessionalHealthThirdPartyId
                    End If
                    .CostValue = serviceOrderDetail.CostValue
                    If ipsTmp.BillingConceptId Is Nothing Then
                        errorsSurgical.AppendLine("El servicio IPS " + ipsTmp.Code + " - " + ipsTmp.Name + " no tiene asignado concepto de facturación")
                        Continue For
                    End If
                    .BillingConceptId = ipsTmp.BillingConceptId
                    .CostCenterId = serviceOrderDetail.CostCenterId
                    If rateManualDetailSurgical IsNot Nothing Then
                        .RateManualDetailSurgicalId = rateManualDetailSurgical.Id
                    End If
                    .SurchargeApply = False
                    .RoundService = round
                End With
                serviceOrderDetail.ServiceOrderDetailSurgical.Add(serviceOrderDetailSurgical)
            Next
            If errorsSurgical.Length > 0 Then
                errorTotalSurgical.AppendLine(String.Format("El item {0} no se pudo homologar por:" & vbCrLf & "{1}", serviceOrderDetail.CodeNameIpsService, errorsSurgical.ToString()))
                Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = False, .Message = errorTotalSurgical.ToString()}
            End If
        Else
            'si el ips es no quirurgico o paquete
            Dim rateManualDetail = _rateManualDetailRepository.GetRateManualDetailServiceOrder(rateManual.Id, IPSServiceId)
            If rateManualDetail Is Nothing Then
                Dim ipsTmp = _ipsServiceRepository.GetIPSServiceById(IPSServiceId)
                'valido que el ips este parametrizado
                Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = False, .Message = String.Format(ResourceManager.GetString("NotParameterizedIPS", MODULE_NAME), ipsTmp.Code + " - " + ipsTmp.Name) + " " + rateManual.Code + " - " + rateManual.Name}
            End If
            'asigno los valores al aentidad
            Dim totalSalesPriceItem = rateManualDetail.SalesValue + (rateManualDetail.SalesValue * (rateVariation / 100))

            If totalSalesPriceItem > rateManualDetail.RateManual.RoundService Then
                totalSalesPriceItem = Utils.RoundValue(totalSalesPriceItem, rateManualDetail.RateManual.RoundService)
            End If

            serviceOrderDetail.RateManualId = rateManual.Id
            serviceOrderDetail.RateManualType = rateManual.Type
            serviceOrderDetail.RateManualDetailId = rateManualDetail.Id
            serviceOrderDetail.RateManualSalePrice = totalSalesPriceItem
            serviceOrderDetail.SubTotalSalesPrice = totalSalesPriceItem
            serviceOrderDetail.TotalSalesPrice = totalSalesPriceItem
            serviceOrderDetail.PerformsHealthProfessionalThirdPartyId = ProfessionalHealthThirdPartyId
            serviceOrderDetail.PerformsHealthProfessionalCode = ProfessionalHealthCode
            serviceOrderDetail.GrandTotalSalesPrice = serviceOrderDetail.TotalSalesPrice * InvoicedQuantity
            serviceOrderDetail.RoundService = 1

            'si el valor total es mayor al redondeo entonces se aplica
            If serviceOrderDetail.GrandTotalSalesPrice > rateManualDetail.RateManual.RoundService Then
                serviceOrderDetail.GrandTotalSalesPrice = Utils.RoundValue(serviceOrderDetail.GrandTotalSalesPrice, rateManualDetail.RateManual.RoundService)
                serviceOrderDetail.RoundService = rateManualDetail.RateManual.RoundService
            End If
        End If
        serviceOrderDetail.RecordType = 1
        serviceOrderDetail.CareGroupId = CareGroupId
        serviceOrderDetail.IPSServiceId = IPSServiceId
        serviceOrderDetail.GrossValue = serviceOrderDetail.SubTotalSalesPrice

        If serviceOrderDetail.Presentation = 2 Then
            InvoicedQuantity = 1
        End If

        serviceOrderDetail.InvoicedQuantity = InvoicedQuantity

        'independientmente si viene de detalles qx o no al final en base a la parametrizacion del servicio y el sistema se establece el valor bruto, el iva y el subtotal
        Dim _dictionaryValues = Utils.SetValueSalesPrice(FlagTaxInclude,
                                                              serviceOrderDetail.SubTotalSalesPrice,
                                                              If(IpsService?.IVAId Is Nothing, 0, IpsService?.GeneralLedgerIVA?.Percentage))
        If IpsService?.TaxedProduct AndAlso _dictionaryValues?.Any() Then
            With serviceOrderDetail
                .GrossValue = _dictionaryValues?.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.GrossValue).Value
                .TaxValue = _dictionaryValues?.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.TaxValue).Value
                .SubTotalSalesPrice = _dictionaryValues?.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.SubtotalSalesPrice).Value
                If .SubTotalSalesPrice > serviceOrderDetail.RoundService Then
                    .SubTotalSalesPrice = Utils.RoundValue(serviceOrderDetail.SubTotalSalesPrice, serviceOrderDetail.RoundService)
                End If
                .TotalSalesPrice = .SubTotalSalesPrice
                .GrandTotalSalesPrice = .TotalSalesPrice * .InvoicedQuantity
            End With
        End If

        serviceOrderDetail.Presentation = IpsService.Presentation
        serviceOrderDetail.ServiceDate = ServiceDate
        serviceOrderDetail.PerformsFunctionalUnitId = FunctionalUnitId
        serviceOrderDetail.PerformsHealthProfessionalCode = ProfessionalHealthCode
        serviceOrderDetail.PerformsProfessionalSpecialty = Specialty
        serviceOrderDetail.BillingConceptId = IpsService.BillingConceptId
        serviceOrderDetail.SettlementType = 1
        Return New ActionResult(Of ServiceOrderDetail) With {.StateResult = True, .ObjectEmbbeded = serviceOrderDetail}
    End Function

    ''' <summary>
    ''' funcion para calcular el valor de un servicio cuando se hace desde el modulo de central de mezclas especificamente desde Contratos de centros de atencion externos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetServiceValueToMS(ContractExternalClientId As Integer, CupsId As Integer, FunctionalUnitId As Integer, Specialty As String, ServiceDate As Date, IPSServiceId As Integer, Optional ManualType As Integer = 0, Optional RiasId As Integer? = Nothing, Optional ContractDescriptionId As Integer? = Nothing) As ActionResult(Of List(Of CupsHomologation)) Implements IBillingServices.GetServiceValueToMS
        'Listado que se retorna
        Dim listHomologation As New List(Of CupsHomologation)
        'Se valida si el cups tiene reglas de tipo servicio ips en la definición de tarifas asociada a el grupo de atención
        Dim listTemp = _definitionRateDetailRepository.GetIPSRulesMs(ContractExternalClientId, ServiceDate, CupsId)
        'guarda el id del manual de tarifa para filtra en el detalle junto con el servicio IPS
        Dim RateManualId As Integer = 0
        'Porcentaje de varicion en la tarifa solo para cuando es estandar o por vigencia
        Dim RateVariation As Decimal = 0
        'variable cuando la definicion de la tarifa es fija, sea por condicion o no 
        Dim SalesValue As Decimal = 0
        Dim SalesValueWithSurcharge As Decimal = 0
        'Si tiene reglas de tipo servicio ips se crea la homologación con los datos
        If listTemp IsNot Nothing AndAlso listTemp.Count > 0 Then
            listTemp.ForEach(Sub(item)
                                 Dim cupsHomologation As New CupsHomologation
                                 With cupsHomologation
                                     .CupsEntityId = item.CUPSEntityId
                                     .CodeNameCupsEntity = item.CUPSEntityCodeName
                                     .IPSServiceId = item.IPSServiceId
                                     .CodeNameIpsService = item.IPSServiceCodeName
                                     .Presentation = item.Presentation
                                 End With
                                 listHomologation.Add(cupsHomologation)
                             End Sub)
        End If
        Dim serviceType As Integer? = Nothing
        Dim rate = GetRateValueCondition(IPSServiceId, CupsId, ContractExternalClientId, FunctionalUnitId, Specialty, ServiceDate, ERateOptions.CUPS, RiasId, ContractDescriptionId)

        If rate.StateResult Then
            If rate.ObjectEmbbededAux IsNot Nothing Then 'Fue por que la tarifa se saco de una condicion
                RateVariation = rate.ObjectEmbbededAux.RateVariation
                If rate.ObjectEmbbededAux.LiquidationType = 1 Then 'Si es Fija
                    serviceType = rate.ObjectEmbbededAux.ManualType
                    SalesValue = rate.ObjectEmbbededAux.SalesValue
                    SalesValueWithSurcharge = rate.ObjectEmbbededAux.SalesValueWithSurcharge
                ElseIf rate.ObjectEmbbededAux.LiquidationType = 2 Then 'Si es estandar
                    serviceType = _rateManualRepository.GetRateManualById(rate.ObjectEmbbededAux.RateManualId, False).Type
                    RateManualId = rate.ObjectEmbbededAux.RateManualId
                ElseIf rate.ObjectEmbbededAux.LiquidationType = 3 Then 'Si es por vigencia
                    Dim rateManualValidity = _rateManualValidityRepository.GetRateManualValidityByIdWithAggregates(rate.ObjectEmbbededAux.RateManualValidityId)
                    Dim rateManualValidityDetail = rateManualValidity.RateManualValidityDetail.Where(Function(d) d.InitialDate <= ServiceDate AndAlso d.EndDate >= ServiceDate).FirstOrDefault
                    If rateManualValidityDetail Is Nothing Then
                        Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = False, .Message = String.Format("No se encuentra parametrizado un manual de tarifas para la vigencia {0} - {1} en la fecha del servicio {2}", rateManualValidity.Code, rateManualValidity.Name, ServiceDate.ToString("yyyy-mm-dd"))}
                    End If
                    serviceType = _rateManualRepository.GetRateManualById(rateManualValidityDetail.RateManualId, False).Type
                    RateManualId = rateManualValidityDetail.RateManualId
                End If
            Else 'Es por que la condicion fue ninguna y la tarifa esta en la cabecera
                RateVariation = rate.ObjectEmbbeded.RateVariation
                If rate.ObjectEmbbeded.LiquidationType = 1 Then 'Si es Fija
                    serviceType = rate.ObjectEmbbeded.ManualType
                    SalesValue = rate.ObjectEmbbeded.SalesValue
                    SalesValueWithSurcharge = rate.ObjectEmbbeded.SalesValueWithSurcharge
                ElseIf rate.ObjectEmbbeded.LiquidationType = 2 Then 'Si es estandar
                    serviceType = _rateManualRepository.GetRateManualById(rate.ObjectEmbbeded.RateManualId, False).Type
                    RateManualId = rate.ObjectEmbbeded.RateManualId
                ElseIf rate.ObjectEmbbeded.LiquidationType = 3 Then 'Si es por vigencia
                    Dim rateManualValidity = _rateManualValidityRepository.GetRateManualValidityByIdWithAggregates(rate.ObjectEmbbeded.RateManualValidityId)
                    Dim rateManualValidityDetail = rateManualValidity.RateManualValidityDetail.Where(Function(d) d.InitialDate <= ServiceDate AndAlso d.EndDate >= ServiceDate).FirstOrDefault
                    If rateManualValidityDetail Is Nothing Then
                        Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = False, .Message = String.Format("No se encuentra parametrizado un manual de tarifas para la vigencia {0} - {1} en la fecha del servicio {2}", rateManualValidity.Code, rateManualValidity.Name, ServiceDate.ToString("yyyy-mm-dd"))}
                    End If
                    serviceType = _rateManualRepository.GetRateManualById(rateManualValidityDetail.RateManualId, False).Type
                    RateManualId = rateManualValidityDetail.RateManualId
                End If
            End If
        ElseIf listTemp Is Nothing OrElse listTemp.Count = 0 Then
            Return New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = False, .Message = rate.Message}
        End If

        If serviceType IsNot Nothing AndAlso serviceType > 0 Then
            'Listado de Cups Homologation con FullNameCupsEntity e FullNameIpsService
            Dim list = _cupsHomologationRepository.ListCupsHomologationByCupsId(CupsId, serviceType)
            If list IsNot Nothing AndAlso list.Count > 0 Then
                list.ForEach(Sub(y)
                                 If (From n In listHomologation Where n.CupsEntityId = y.CupsEntityId AndAlso n.IPSServiceId = y.IPSServiceId).Count = 0 Then
                                     listHomologation.Add(y)
                                 End If
                             End Sub)
            End If

            If listHomologation IsNot Nothing AndAlso listHomologation.Count = 1 Then
                With listHomologation.FirstOrDefault
                    If SalesValue > 0 Or SalesValueWithSurcharge > 0 Then
                        .SubTotalSalesValue = SalesValue
                        .SubTotalSalesValueWithSurcharge = SalesValueWithSurcharge
                    Else
                        Dim RateManualDetail = _rateManualDetailRepository.FirstOrDefault(Function(x) x.RateManualId = RateManualId And x.IPSServiceId = .IPSServiceId)
                        .SubTotalSalesValue = (RateManualDetail.SalesValue * (RateVariation / 100)) + RateManualDetail.SalesValue
                        .SubTotalSalesValueWithSurcharge = (RateManualDetail.SalesValueWithSurcharge * (RateVariation / 100)) + RateManualDetail.SalesValueWithSurcharge
                    End If
                End With

            End If

            If serviceType = ManualType Then
                Dim itemReturn = listHomologation.Find(Function(x) x.IPSServiceId = IPSServiceId)
                listHomologation.Clear()
                If itemReturn IsNot Nothing Then
                    listHomologation.Add(itemReturn)
                End If
            End If
        End If

        Dim result As New ActionResult(Of List(Of CupsHomologation)) With {.StateResult = True, .ObjectEmbbeded = listHomologation}

        If listHomologation Is Nothing OrElse listHomologation.Count = 0 OrElse listHomologation.Count > 1 Then
            Dim ipsServiceFullName As String = String.Empty
            If IPSServiceId <> 0 Then
                Dim ipsService As IPSService = GetIpsService(IPSServiceId)
                If ipsService IsNot Nothing AndAlso ipsService.Id > 0 Then
                    ipsServiceFullName = String.Concat(ipsService.Code, " - ", ipsService.Name)
                End If
            Else
                Dim cups As CUPSEntity = GetCupsEntity(CupsId, False)
                If cups IsNot Nothing AndAlso cups.Id > 0 Then
                    ipsServiceFullName = String.Concat(cups.Code, " - ", cups.Description)
                End If
            End If
            Dim ContractExternalClients As ContractExternalClients = GetContractExternalClient(ContractExternalClientId)
            Dim ExternalContractFullName As String = String.Concat(ContractExternalClients.Code, " - ", ContractExternalClients.ContractNumber)
            Dim _message = IIf(listHomologation.Count > 1, "Se encontró mas de 1 Homologación", "No se Encontraron Homologos")
            result.Message = String.Format(" {0} para el item {1} para el Contrato de Centro de Atención Externo {2}", _message, ipsServiceFullName, ExternalContractFullName)
            result.StateResult = False
        End If
        Return result
    End Function

    Private Function GetRateValueCondition(IPSServiceId As Integer, CupsEntityId As Integer, ContractExternalClientId As Integer, FunctionalUnitId As Integer?, Specialty As String, ServiceDate As Date, initialService As ERateOptions, RiasId As Integer?, ContractDescriptionId As Integer?) As ActionResult(Of DefinitionRateDetail, DefinitionRateDetailCondition)
        Dim result As ActionResult(Of DefinitionRateDetail, DefinitionRateDetailCondition)
        Dim rateDetails As List(Of DefinitionRateDetail) = New List(Of DefinitionRateDetail)()

        Dim definitionRate As ContractExternalClientsDefinitionRate = GetContractExternalCDefinitionRate(ContractExternalClientId, ServiceDate)
        If definitionRate.Id = 0 Then
            result = New ActionResult(Of DefinitionRateDetail, DefinitionRateDetailCondition) With {.StateResult = False, .Message = "No se encontro una definicion de tarifas en la fecha " + ServiceDate.Date.ToString}
            Return result
        End If

        rateDetails = _contractServices.GetDefinitionRateDetailList(definitionRate.DefinitionRateId, CupsEntityId, initialService, IPSServiceId)

        'Recorro los detalle para empezar a verificar las condiciones
        For Each detail In rateDetails
            If detail.LogicalOperator = 1 Then 'Si el operador logico es ninguna, quiere decir que solo maneja una condicion
                If detail.ConditionType = EConditionType.Null Then 'Si es ninguna la liquidacion esta en la misma tabla de detalle
                    result = New ActionResult(Of DefinitionRateDetail, DefinitionRateDetailCondition) With {.StateResult = True, .ObjectEmbbeded = detail}
                    Return result
                Else

                    Dim rateCondition As DefinitionRateDetailCondition = Nothing
                    Select Case detail.ConditionType
                        Case EConditionType.Hour
                            rateCondition = _definitionRateDetailConditionRepository.GetDefinitionRateDetailConditionByTime(detail.Id, ServiceDate.TimeOfDay)
                        Case EConditionType.Specialty
                            rateCondition = _definitionRateDetailConditionRepository.GetDefinitionRateDetailConditionBySpecialty(detail.Id, Specialty)
                        Case EConditionType.FunctionalUnit
                            rateCondition = _definitionRateDetailConditionRepository.GetDefinitionRateDetailConditionByFunctionalUnit(detail.Id, FunctionalUnitId)
                        Case EConditionType.UnitType
                            Dim functionalUnit = _functionalUnitRepository.GetFunctionalUnitById(FunctionalUnitId, False)
                            rateCondition = _definitionRateDetailConditionRepository.GetDefinitionRateDetailConditionByUnitType(detail.Id, functionalUnit.UnitType)
                        Case EConditionType.Rias
                            If RiasId Is Nothing Then
                                Continue For
                            End If
                            rateCondition = _definitionRateDetailConditionRepository.GetDefinitionRateDetailConditionByRiasId(detail.Id, RiasId)
                        Case EConditionType.Description
                            If ContractDescriptionId Is Nothing Then
                                Continue For
                            End If
                            rateCondition = _definitionRateDetailConditionRepository.GetDefinitionRateDetailConditionByDescriptionId(detail.Id, ContractDescriptionId)
                    End Select
                    If rateCondition IsNot Nothing AndAlso rateCondition.Id > 0 Then
                        result = New ActionResult(Of DefinitionRateDetail, DefinitionRateDetailCondition) With {.StateResult = True, .ObjectEmbbeded = detail, .ObjectEmbbededAux = rateCondition}
                        Return result
                    End If
                End If
            Else 'Entonces maneja dos condiciones

                Dim rateCondition As DefinitionRateDetailCondition = Nothing
                Dim rateConditionList As List(Of DefinitionRateDetailCondition) = _contractServices.GetDefinitionRateDetailByDefinitionRateDetailId(detail.Id)
                Select Case detail.ConditionType
                    Case 1 'Horario
                        Select Case detail.ConditionType2
                            Case 2 'Horario y Especialidad
                                rateCondition = _contractServices.getConditionByTimeSpecialty(ServiceDate.TimeOfDay, Specialty, rateConditionList)
                            Case 3 'Horario y Unidad Funcional
                                rateCondition = _contractServices.getConditionByTimeFunctionalUnit(ServiceDate.TimeOfDay, FunctionalUnitId, rateConditionList)
                            Case 4 'Horario y Tipo de unidad
                                Dim functionalUnit = _functionalUnitRepository.GetFunctionalUnitById(FunctionalUnitId, False)
                                rateCondition = _contractServices.getConditionByTimeUnitType(ServiceDate.TimeOfDay, functionalUnit.UnitType, rateConditionList)
                            Case 6 'Horario y Rias
                                If RiasId Is Nothing Then
                                    Continue For
                                End If
                                rateCondition = _contractServices.getConditionByTimeRias(ServiceDate.TimeOfDay, RiasId, rateConditionList)
                            Case 7 'Horario y Descripción
                                If ContractDescriptionId Is Nothing Then
                                    Continue For
                                End If
                                rateCondition = _contractServices.getConditionByTimeDescription(ServiceDate.TimeOfDay, ContractDescriptionId, rateConditionList)
                        End Select
                    Case 2 'Especialidad
                        Select Case detail.ConditionType2
                            Case 1 'Especialidad y Horario
                                rateCondition = _contractServices.getConditionBySpecialtyTime(Specialty, ServiceDate.TimeOfDay, rateConditionList)
                            Case 3 'Especialidad y Unidad Funcional
                                rateCondition = _contractServices.getConditionBySpecialtyFunctionalUnit(Specialty, FunctionalUnitId, rateConditionList)
                            Case 4 'Especialidad y Tipo de unidad
                                Dim functionalUnit = _functionalUnitRepository.GetFunctionalUnitById(FunctionalUnitId, False)
                                rateCondition = _contractServices.getConditionBySpecialtyUnitType(Specialty, functionalUnit.UnitType, rateConditionList)
                            Case 6 'Especialidad y Rias
                                If RiasId Is Nothing Then
                                    Continue For
                                End If
                                rateCondition = _contractServices.getConditionBySpecialtyRias(Specialty, RiasId, rateConditionList)
                            Case 7 'Especialidad y Description
                                If ContractDescriptionId Is Nothing Then
                                    Continue For
                                End If
                                rateCondition = _contractServices.getConditionBySpecialtyDescription(Specialty, ContractDescriptionId, rateConditionList)
                        End Select
                    Case 3 'Unidad Funcional
                        Select Case detail.ConditionType2
                            Case 1 'Unidad Funcional y Horario
                                rateCondition = _contractServices.getConditionByFunctionalUnitTime(FunctionalUnitId, ServiceDate.TimeOfDay, rateConditionList)
                            Case 2 'Unidad Funcional y Especialidad
                                rateCondition = _contractServices.getConditionByFunctionalUnitSpecialty(FunctionalUnitId, Specialty, rateConditionList)
                            Case 4 'Unidad Funcional y Tipo de unidad
                                Dim functionalUnit = _functionalUnitRepository.GetFunctionalUnitById(FunctionalUnitId, False)
                                rateCondition = _contractServices.getConditionByFunctionalUnitUnitType(FunctionalUnitId, functionalUnit.UnitType, rateConditionList)
                            Case 6 'Unidad Funcional y Rias
                                If RiasId Is Nothing Then
                                    Continue For
                                End If
                                rateCondition = _contractServices.getConditionByFunctionalUnitRias(FunctionalUnitId, RiasId, rateConditionList)
                            Case 7 'Unidad Funcional y Description
                                If ContractDescriptionId Is Nothing Then
                                    Continue For
                                End If
                                rateCondition = _contractServices.getConditionByFunctionalUnitDescription(FunctionalUnitId, ContractDescriptionId, rateConditionList)
                        End Select
                    Case 4 'Tipo de Unidad
                        Dim functionalUnit = _functionalUnitRepository.GetFunctionalUnitById(FunctionalUnitId, False)
                        Select Case detail.ConditionType2
                            Case 1 'Tipo de Unidad y Horario
                                rateCondition = _contractServices.getConditionByUnitTypeTime(functionalUnit.UnitType, ServiceDate.TimeOfDay, rateConditionList)
                            Case 2 'Tipo de Unidad y Especialidad
                                rateCondition = _contractServices.getConditionByUnitTypeSpecialty(functionalUnit.UnitType, Specialty, rateConditionList)
                            Case 3 'Tipo de Unidad y Unidad Funcional
                                rateCondition = _contractServices.getConditionByUnitTypeFunctionalUnit(functionalUnit.UnitType, FunctionalUnitId, rateConditionList)
                            Case 6 'Tipo de Unidad y Rias
                                If RiasId Is Nothing Then
                                    Continue For
                                End If
                                rateCondition = _contractServices.getConditionByUnitTypeRias(functionalUnit.UnitType, RiasId, rateConditionList)
                            Case 7 'Tipo de Unidad y Description
                                If ContractDescriptionId Is Nothing Then
                                    Continue For
                                End If
                                rateCondition = _contractServices.getConditionByUnitTypeDescription(functionalUnit.UnitType, ContractDescriptionId, rateConditionList)
                        End Select
                    Case 6 'Rias
                        If RiasId Is Nothing Then
                            Continue For
                        End If
                        Select Case detail.ConditionType2
                            Case 1 'Rias y horario
                                rateCondition = _contractServices.getConditionByRiasTime(RiasId, ServiceDate.TimeOfDay, rateConditionList)
                            Case 2 'Rias y Especialidad
                                rateCondition = _contractServices.getConditionByRiasSpecialty(RiasId, Specialty, rateConditionList)
                            Case 3 'Rias y Unidad Funcional
                                rateCondition = _contractServices.getConditionByRiasFunctionalUnit(RiasId, FunctionalUnitId, rateConditionList)
                            Case 4 'Rias y Tipo de Unidad
                                Dim functionalUnit = _functionalUnitRepository.GetFunctionalUnitById(FunctionalUnitId, False)
                                rateCondition = _contractServices.getConditionByRiasUnitType(RiasId, functionalUnit.UnitType, rateConditionList)
                            Case 7 'Rias y Descripcion
                                rateCondition = _contractServices.getConditionByRiasDescription(RiasId, ContractDescriptionId, rateConditionList)
                        End Select
                    Case 7 'Descriptions
                        If ContractDescriptionId Is Nothing Then
                            Continue For
                        End If
                        Select Case detail.ConditionType2
                            Case 1 'Description y horario
                                rateCondition = _contractServices.getConditionByDescriptionTime(ContractDescriptionId, ServiceDate.TimeOfDay, rateConditionList)
                            Case 2 'Description y Especialidad
                                rateCondition = _contractServices.getConditionByDescriptionSpecialty(ContractDescriptionId, Specialty, rateConditionList)
                            Case 3 'Description y Unidad Funcional
                                rateCondition = _contractServices.getConditionByDescriptionFunctionalUnit(ContractDescriptionId, FunctionalUnitId, rateConditionList)
                            Case 4 'Description y Tipo de Unidad
                                Dim functionalUnit = _functionalUnitRepository.GetFunctionalUnitById(FunctionalUnitId, False)
                                rateCondition = _contractServices.getConditionByDescriptionUnitType(ContractDescriptionId, functionalUnit.UnitType, rateConditionList)
                            Case 6 'Description y Rias
                                If RiasId Is Nothing Then
                                    Continue For
                                End If
                                rateCondition = _contractServices.getConditionByDescriptionRias(ContractDescriptionId, RiasId, rateConditionList)
                        End Select
                End Select
                If rateCondition IsNot Nothing AndAlso rateCondition.Id > 0 Then
                    result = New ActionResult(Of DefinitionRateDetail, DefinitionRateDetailCondition) With {.StateResult = True, .ObjectEmbbeded = detail, .ObjectEmbbededAux = rateCondition}
                    Return result
                End If
            End If
        Next
        If initialService = ERateOptions.GENERAL Then
            Dim cups = GetCupsEntity(CupsEntityId, False)
            Return New ActionResult(Of DefinitionRateDetail, DefinitionRateDetailCondition) With {.StateResult = False, .Message = "No se encontro una tarifa para el CUPS " & cups.Code & " - " & cups.Description, .MessageResult = {"ERROR1"}.ToList}
        Else
            Return GetRateValueCondition(IPSServiceId, CupsEntityId, ContractExternalClientId, FunctionalUnitId, Specialty, ServiceDate, initialService + 1, RiasId, ContractDescriptionId)
        End If
    End Function


#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                _contractServices.Dispose()
            End If
            _revenueControlDetailRepository = Nothing
            _rateManualValidityRepository = Nothing
            _rateManualRepository = Nothing
            _cupsHomologation = Nothing
            _functionalUnitRepository = Nothing
            _costCenterRepository = Nothing
            _cupsRepository = Nothing
            _ipsServiceRepository = Nothing
            _surgicalProcedureServiceRepository = Nothing
            _rateManualDetailSurgicalRepository = Nothing
            _rateManualDetailRepository = Nothing
            _productRateDetailRepository = Nothing
            _careGroupRepository = Nothing
            _inventoryProductRepository = Nothing
            _serviceOrderDetailDistributionRepository = Nothing
            _revenueControl = Nothing
            _patientRepository = Nothing
            _billingAuthorizationRepository = Nothing
            _settingsBillingRepository = Nothing
            _serviceOrderDetailRepository = Nothing
            _mainAccountsRepository = Nothing
            _settingsInventoryRepository = Nothing
            _customerRepository = Nothing
            _contractServices = Nothing
            _productGroupRepository = Nothing
            _iNPACIENTTOPANURepository = Nothing
            _billingConceptRepository = Nothing
            _invoiceRepository = Nothing
            _careGroupDefinitionRateRepository = Nothing
            _definitionRateDetailConditionRepository = Nothing
            _definitionRateDetailRepository = Nothing
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

#Region "Enums"
Public Enum EConditionType As Integer
    ''' <summary>
    ''' horario
    ''' </summary>
    ''' <remarks></remarks>
    Hour = 1
    ''' <summary>
    ''' Especialidad
    ''' </summary>
    ''' <remarks></remarks>
    Specialty = 2
    ''' <summary>
    ''' Unidad Funcional
    ''' </summary>
    ''' <remarks></remarks>
    FunctionalUnit = 3
    ''' <summary>
    ''' Tipo de Unidad
    ''' </summary>
    ''' <remarks></remarks>
    UnitType = 4
    ''' <summary>
    ''' Ninguna
    ''' </summary>
    ''' <remarks></remarks>
    Null = 5
    ''' <summary>
    ''' Rias
    ''' </summary>
    ''' <remarks></remarks>
    Rias = 6
    ''' <summary>
    ''' Descripciones
    ''' </summary>
    ''' <remarks></remarks>
    Description = 7
End Enum
''' <summary>
''' opciones de tarifa
''' </summary>
''' <remarks></remarks>
Public Enum ERateOptions As Integer
    IPSSERVICE = 0
    CUPS = 1
    SUBGROUP = 2
    GROUP = 3
    GENERAL = 4
End Enum

Public Enum eCareGroupType
    EAPBWithContract = 1
    EAPBWithOutContract = 2
    Particular = 3
    Aseguradora = 4
End Enum

''' <summary>
''' Enumeracion para saber en que caso se genera la cuenta por cobrar
''' </summary>
Public Enum eAccountReceivableGenerate
    ThirdParty = 1 'Entidad
    Patient = 2 'Paciente
    Pagare = 3 'Pagare
End Enum

''' <summary>
''' Enumeración para tipo de servicio
''' </summary>
Public Enum eRecordType
    Services = 1
    Medicamentos = 2
End Enum

Public Enum eSettlementType
    ManualDeTarifas = 1
    PorcentajeOtroServicio = 2
    IncluidoOtroServicio = 3
    PorcentajeMismoServicio = 4
End Enum

#End Region

#Region "XML"

<Serializable(), XmlRoot("ListServiceOrderDetailSurgical"), XmlType("ListServiceOrderDetailSurgical")>
Public Class ListServiceOrderDetailSurgicalXml
    <XmlElement("ServiceOrderDetailSurgical")>
    Property ServiceOrderDetailSurgicalXml As New List(Of ServiceOrderDetailSurgicalXml)
End Class

<Serializable(), XmlType("ServiceOrderDetailSurgical")>
Public Class ServiceOrderDetailSurgicalXml
    Property RowId As Integer
    Property CodeNameIpsService As String
    Property IPSServiceId As Integer
    Property InvoicedQuantity As Integer
    Property LiquidationPercentage As Decimal
    Property TotalSalesPrice As Decimal
    Property ClassServiceIps As String
    Property RateManualSalePrice As Decimal
    Property PerformsHealthProfessionalCode As String
    Property PerformsHealthProfessionalThirdPartyId As Integer?
    Property CostValue As Decimal
    Property BillingConceptId As Integer
    Property CostCenterId As Integer
    Property RateManualDetailSurgicalId As Integer?
    Property SurchargeApply As Boolean
    Property IncomeMainAccountId As Integer
    Property RoundService As Integer
    Property AllowValueChange As Boolean
End Class

#End Region