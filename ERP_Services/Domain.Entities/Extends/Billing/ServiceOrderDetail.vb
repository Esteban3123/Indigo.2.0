Imports System.Runtime.Serialization
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

Partial Public Class ServiceOrderDetail
    Inherits Entity(Of Domain.Entities.ServiceOrderDetail)

    <DataMember()>
    Public Property OperatingUnitId As Integer

    <DataMember()>
    Public Property PatientCode As String

    <DataMember()>
    Public Property AdmissionNumber As String

    Public Property RowXml As Integer

    <DataMember()>
    Public Property IdTmp As Integer
    <DataMember()>
    Public Property ItemCode As String

    <DataMember()>
    Public Property ItemDescription As String

    <DataMember()>
    Public Property CodeNameIpsService As String

    <DataMember()>
    Public Property CodeNameCups As String

    <DataMember()>
    Public Property CodeNameCareGroup As String

    <DataMember()>
    Public Property CodeNameCostCenter As String

    <DataMember()>
    Public Property CodeNameFunctionalUnit As String

    <DataMember()>
    Public Property CodeNameHealthProfessional As String

    <DataMember()>
    Public Property CodeNameSpeciality As String

    <DataMember()>
    Public Property AllowValueChange As Boolean

    <DataMember()>
    Public Property IsSOAT As Boolean

    <DataMember>
    Public Property CodeNameProduct As String

    '<DataMember()>
    'Public Property CareGroupWithoutContractThirdPartyId As Integer?

    '<DataMember()>
    'Public Property NitNameCareGroupWithoutContractThirdParty As String

    <DataMember()>
    Public Property CodeNameHealthAdministrator As String

    <DataMember()>
    Public Property NitNameThirdParty As String

    <DataMember()>
    Public Property IdCita As String
    ''' <summary>
    ''' Código del cups que genera el detalle de la orden de servicio
    ''' </summary>
    <DataMember()>
    Public Property CODSERIPS As String

    ''' <summary>
    ''' Cantidad realizada del cups para el proceso de RIAS
    ''' </summary>
    <DataMember()>
    Public Property RealizedQuantity As Integer

    ''' <summary>
    ''' Prop utilizada cuando en la retarificacion hay quirurgicos, al volver a enviar al servidor saber con cual asociar
    ''' </summary>
    ''' <value>
    ''' The previus service order detail identifier.
    ''' </value>
    <DataMember()>
    Public Property PreviusServiceOrderDetailId As Integer?

    ''' <summary>
    ''' Descripción de la rias
    ''' </summary>
    <DataMember()>
    Public Property RiasCupsDescription As String

    ''' <summary>
    ''' Código y nombre de la descripción del módulo de contratos
    ''' </summary>
    <DataMember()>
    Public Property ContractDescriptionCodeName As String

    ''' <summary>
    ''' Id de la cotización
    ''' </summary>
    <DataMember()>
    Public Property QuotationId As Integer

    ''' <summary>
    ''' Id de la autorización de servicio tercerizado
    ''' </summary>
    <DataMember()>
    Public Property AuthorizationOutsourcedServicesId As Integer

    ''' <summary>
    ''' Código de la cotización
    ''' </summary>
    <DataMember()>
    Public Property QuotationCode As String

    ''' <summary>
    ''' Liquidar todas las cirugías MIVIE.
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property LiquidateAllMIVIE As Boolean?

    ''' <summary>
    ''' Ids de los detalles que se estan editando para la validación de numero de autorización
    ''' </summary>
    <DataMember()>
    Public Property ExcludeIds As String

    ''' <summary>
    ''' propiedad extendida que establece si el servicio es gravado con impuestos o no
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property TaxedService As Boolean

    ''' <summary>
    ''' variable que guarda el procentaje del impuesto del servicio
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property TaxPercent As Decimal
#Region "PropertiesMedicalFees"

    'Estas propiedades se utilizan en el modulo de MedicalFees

    <DataMember()>
    Public Property BillingConceptDescription As String

    <DataMember()>
    Public Property IPSServiceDescription As String

    <DataMember()>
    Public Property MedicDescription As String

    <DataMember()>
    Public Property SelectOption As Boolean

    <DataMember()>
    Public Property CausedValue As Decimal

#End Region

    <DataMember()>
    Public Property HemocomponentId As Integer

    <DataMember()>
    Public Property IsServiceOrderDetailControlJustify As Boolean

    <DataMember()>
    Public Property ServiceOrderDetailControlJustification As String

    ''' <summary>
    ''' Crea una copia de la entidad
    ''' </summary>
    ''' <returns>Copia de la entidad</returns>
    Public Function CloneEntity() As ServiceOrderDetail
        Dim entity As ServiceOrderDetail = DirectCast(MemberwiseClone(), ServiceOrderDetail)
        Return entity
    End Function

    Public Function ToXml() As String
        Dim surgicalXml As String = Nothing
        If Presentation = 2 AndAlso ServiceOrderDetailSurgical IsNot Nothing AndAlso ServiceOrderDetailSurgical.Any() Then
            surgicalXml = "<ServiceOrderDetailSurgicalXml>" & String.Join(vbCrLf,
                                      ServiceOrderDetailSurgical.Select(Function(o)
                                                                            Return $"
		<ServiceOrderDetailSurgical>
			<Id>{o.Id}</Id>            
			<ServiceOrderDetailId>{o.ServiceOrderDetailId}</ServiceOrderDetailId>
            {If(o.CodeNameIpsService IsNot Nothing, $"<CodeNameIpsService>{o.CodeNameIpsService}</CodeNameIpsService>", "")}
			<IPSServiceId>{o.IPSServiceId}</IPSServiceId>
			<InvoicedQuantity>{o.InvoicedQuantity}</InvoicedQuantity>
			<LiquidationPercentage>{o.LiquidationPercentage.ToString().Replace(",", ".")}</LiquidationPercentage>
			<RateManualSalePrice>{o.RateManualSalePrice.ToString().Replace(",", ".")}</RateManualSalePrice>
			<TotalSalesPrice>{o.TotalSalesPrice.ToString().Replace(",", ".")}</TotalSalesPrice>
            {If(o.PerformsHealthProfessionalCode IsNot Nothing, $"<PerformsHealthProfessionalCode>{o.PerformsHealthProfessionalCode}</PerformsHealthProfessionalCode>", "")}				
            {If(o.PerformsHealthProfessionalThirdPartyId IsNot Nothing, $"<PerformsHealthProfessionalThirdPartyId>{o.PerformsHealthProfessionalThirdPartyId}</PerformsHealthProfessionalThirdPartyId>", "")}
			<CostValue>{o.CostValue.ToString().Replace(",", ".")}</CostValue>
			<BillingConceptId>{o.BillingConceptId}</BillingConceptId>
			<CostCenterId>{o.CostCenterId}</CostCenterId>
            {If(o.RateManualDetailSurgicalId IsNot Nothing, $"<RateManualDetailSurgicalId>{o.RateManualDetailSurgicalId}</RateManualDetailSurgicalId>", "")}
			<SurchargeApply>{If(o.SurchargeApply, 1, 0)}</SurchargeApply>
			<IncomeMainAccountId>{o.IncomeMainAccountId}</IncomeMainAccountId>
			<ClassServiceIps>{o.ClassServiceIps}</ClassServiceIps>
            <RoundService>{o.RoundService}</RoundService>
		</ServiceOrderDetailSurgical>"
                                                                        End Function).ToArray()) & "</ServiceOrderDetailSurgicalXml>"

        End If

        Dim formatSod As String = $"
    <ServiceOrderDetail>
		<RowId>{RowXml}</RowId>
		<Id>{Id}</Id>
		<ServiceOrderId>{ServiceOrderId}</ServiceOrderId>
        <OperatingUnitId>{OperatingUnitId}</OperatingUnitId>
        <PatientCode>{PatientCode}</PatientCode>
        <AdmissionNumber>{AdmissionNumber}</AdmissionNumber>
		<CareGroupId>{CareGroupId}</CareGroupId>
        <ExcludeIds>{ExcludeIds}</ExcludeIds>
        {If(HealthAdministratorId IsNot Nothing, $"<HealthAdministratorId>{HealthAdministratorId}</HealthAdministratorId>", Nothing)}
        {If(ThirdPartyId IsNot Nothing, $"<ThirdPartyId>{ThirdPartyId}</ThirdPartyId>", "")}
		<ServiceType>{ServiceType}</ServiceType>
		<RecordType>{RecordType}</RecordType>
        {If(CUPSEntityId IsNot Nothing, $"<CUPSEntityId>{CUPSEntityId}</CUPSEntityId>", "")}
        {If(IPSServiceId IsNot Nothing, $"<IPSServiceId>{IPSServiceId}</IPSServiceId>", "")}
        {If(HospitalStayId IsNot Nothing, $"<HospitalStayId>{HospitalStayId}</HospitalStayId>", "")}
        {If(HospitalStayDetailId IsNot Nothing, $"<HospitalStayDetailId>{HospitalStayDetailId}</HospitalStayDetailId>", "")}
        {If(ControlExternalConsultation IsNot Nothing, $"<ControlExternalConsultation>{ControlExternalConsultation}</ControlExternalConsultation>", "")}
        {If(ControlExternalConsultationCode IsNot Nothing, $"<ControlExternalConsultationCode>{ControlExternalConsultationCode}</ControlExternalConsultationCode>", "")}
		<CUPSAssociateService>{If(CUPSAssociateService, 1, 0)}</CUPSAssociateService>
        {If(CodeAssociateService IsNot Nothing, $"<CodeAssociateService>{CodeAssociateService}</CodeAssociateService>", "")}
		<IsPackage>{If(IsPackage, 1, 0)}</IsPackage>
		<Packaging>{If(Packaging, 1, 0)}</Packaging>
        {If(PackageServiceOrderDetailId IsNot Nothing, $"<PackageServiceOrderDetailId>{PackageServiceOrderDetailId}</PackageServiceOrderDetailId>", "")}
		<LiquidationType>{LiquidationType}</LiquidationType>
        {If(Presentation IsNot Nothing, $"<Presentation>{Presentation}</Presentation>", "")}
        {If(ProductId IsNot Nothing, $"<ProductId>{ProductId}</ProductId>", "")}
		<InvoicedQuantity>{InvoicedQuantity}</InvoicedQuantity>
		<SupplyQuantity>{SupplyQuantity}</SupplyQuantity>
		<DevolutionQuantity>{DevolutionQuantity}</DevolutionQuantity>
		<RateManualSalePrice>{RateManualSalePrice.ToString().Replace(",", ".")}</RateManualSalePrice>
		<CostValue>{CostValue.ToString().Replace(",", ".")}</CostValue>
		<ServiceDate>{ServiceDate.ToString("dd/MM/yyyy HH:mm:ss")}</ServiceDate>
        {If(AuthorizationNumber IsNot Nothing, $"<AuthorizationNumber>{AuthorizationNumber}</AuthorizationNumber>", "")}
		<PerformsFunctionalUnitId>{PerformsFunctionalUnitId}</PerformsFunctionalUnitId>
        {If(PerformsHealthProfessionalCode IsNot Nothing, $"<PerformsHealthProfessionalCode>{PerformsHealthProfessionalCode}</PerformsHealthProfessionalCode>", "")}
        {If(PerformsProfessionalSpecialty IsNot Nothing, $"<PerformsProfessionalSpecialty>{PerformsProfessionalSpecialty}</PerformsProfessionalSpecialty>", "")}
        {If(PerformsHealthProfessionalThirdPartyId IsNot Nothing, $"<PerformsHealthProfessionalThirdPartyId>{PerformsHealthProfessionalThirdPartyId}</PerformsHealthProfessionalThirdPartyId>", "")}		
        {If(BillingConceptId IsNot Nothing, $"<BillingConceptId>{BillingConceptId}</BillingConceptId>", "")}
		<CostCenterId>{CostCenterId}</CostCenterId>
		<SettlementType>{SettlementType}</SettlementType>
        {If(IncludeServiceOrderDetailId IsNot Nothing, $"<IncludeServiceOrderDetailId>{IncludeServiceOrderDetailId}</IncludeServiceOrderDetailId>", "")}
        {If(RecoveryRatio IsNot Nothing, $"<RecoveryRatio>{RecoveryRatio}</RecoveryRatio>", "")}
        {If(RateManualId IsNot Nothing, $"<RateManualId>{RateManualId}</RateManualId>", "")}
        {If(RateManualType IsNot Nothing, $"<RateManualType>{RateManualType}</RateManualType>", "")}
        {If(RateManualDetailId IsNot Nothing, $"<RateManualDetailId>{RateManualDetailId}</RateManualDetailId>", "")}
        {If(DefinitionRateDetailId IsNot Nothing, $"<DefinitionRateDetailId>{DefinitionRateDetailId}</DefinitionRateDetailId>", "")}
        {If(DefinitionRateDetailConditionId IsNot Nothing, $"<DefinitionRateDetailConditionId>{DefinitionRateDetailConditionId}</DefinitionRateDetailConditionId>", "")}
		<SubTotalSalesPrice>{SubTotalSalesPrice.ToString().Replace(",", ".")}</SubTotalSalesPrice>
		<ThirdPartyDiscount>{ThirdPartyDiscount.ToString().Replace(",", ".")}</ThirdPartyDiscount>
		<ThirdPartyDiscountPercentage>{ThirdPartyDiscountPercentage.ToString().Replace(",", ".")}</ThirdPartyDiscountPercentage>
		<TotalSalesPrice>{TotalSalesPrice.ToString().Replace(",", ".")}</TotalSalesPrice>
		<GrandTotalSalesPrice>{GrandTotalSalesPrice.ToString().Replace(",", ".")}</GrandTotalSalesPrice>
        <GrossValue>{GrossValue.ToString().Replace(",", ".")}</GrossValue>
        <TaxValue>{TaxValue.ToString().Replace(",", ".")}</TaxValue>
        <IvaId>{IvaId}</IvaId>     
		<SurchargeApply>{If(SurchargeApply, 1, 0)}</SurchargeApply>
        {If(SurgicalInterventionType IsNot Nothing, $"<SurgicalInterventionType>{SurgicalInterventionType}</SurgicalInterventionType>", "")}
		<SurgeryNumber>{SurgeryNumber}</SurgeryNumber>
		<IsFirstEvent>{If(IsFirstEvent, 1, 0)}</IsFirstEvent>
		<IsAnnulled>{If(IsAnnulled, 1, 0)}</IsAnnulled>
		<IsDelete>{If(IsDelete, 1, 0)}</IsDelete>
		<IncomeMainAccountId>{IncomeMainAccountId}</IncomeMainAccountId>
		<CodeNameFunctionalUnit>{CodeNameFunctionalUnit}</CodeNameFunctionalUnit>
        {If(PreviusServiceOrderDetailId IsNot Nothing, $"<PreviusServiceOrderDetailId>{PreviusServiceOrderDetailId}</PreviusServiceOrderDetailId>", "")}
		<CodeNameCareGroup>{CodeNameCareGroup}</CodeNameCareGroup>
		<CodeNameCostCenter>{CodeNameCostCenter}</CodeNameCostCenter>
		<CodeNameCups>{CleanSpecialChars(CodeNameCups)}</CodeNameCups>
		<CodeNameIpsService>{CleanSpecialChars(CodeNameIpsService)}</CodeNameIpsService>
		<IsSOAT>{If(IsSOAT, 1, 0)}</IsSOAT>
        <RoundService>{RoundService}</RoundService>
        <CUPSEntityContractDescriptionId>{CUPSEntityContractDescriptionId}</CUPSEntityContractDescriptionId>
        <IsServiceOrderDetailControlJustify>{IsServiceOrderDetailControlJustify}</IsServiceOrderDetailControlJustify>
        <ServiceOrderDetailControlJustification>{ServiceOrderDetailControlJustification}</ServiceOrderDetailControlJustification>
        {If(surgicalXml IsNot Nothing, surgicalXml, "")}
	</ServiceOrderDetail>"
        Return formatSod
    End Function

    Public Sub New()

    End Sub

    Public Sub New(dataXml As Xml.XmlNode)
        Me.RowXml = dataXml.SelectSingleNode("RowId").AsInt()
        Me.Id = dataXml.SelectSingleNode("Id").AsInt()
        Me.ServiceOrderId = dataXml.SelectSingleNode("ServiceOrderId").AsInt()
        Me.CareGroupId = dataXml.SelectSingleNode("CareGroupId").AsInt()
        Me.HealthAdministratorId = dataXml.SelectSingleNode("HealthAdministratorId").AsInt()
        Me.ThirdPartyId = dataXml.SelectSingleNode("ThirdPartyId").AsInt()
        Me.ServiceType = dataXml.SelectSingleNode("ServiceType").AsByte()
        Me.RecordType = dataXml.SelectSingleNode("RecordType").AsByte()
        Me.CUPSEntityId = dataXml.SelectSingleNode("CUPSEntityId").AsInt()
        Me.IPSServiceId = dataXml.SelectSingleNode("IPSServiceId").AsInt()
        Me.CUPSAssociateService = dataXml.SelectSingleNode("CUPSAssociateService").AsBoolean()
        Me.CodeAssociateService = dataXml.SelectSingleNode("CodeAssociateService").AsString()
        Me.IsPackage = dataXml.SelectSingleNode("IsPackage").AsBoolean()
        Me.Packaging = dataXml.SelectSingleNode("Packaging").AsBoolean()
        Me.LiquidationType = dataXml.SelectSingleNode("LiquidationType").AsByte()
        Me.Presentation = dataXml.SelectSingleNode("Presentation").AsByte()
        Me.InvoicedQuantity = dataXml.SelectSingleNode("InvoicedQuantity").AsInt()
        Me.SupplyQuantity = dataXml.SelectSingleNode("SupplyQuantity").AsInt()
        Me.DevolutionQuantity = dataXml.SelectSingleNode("DevolutionQuantity").AsInt()
        Me.RateManualSalePrice = dataXml.SelectSingleNode("RateManualSalePrice").AsDecimal()
        Me.CostValue = dataXml.SelectSingleNode("CostValue").AsDecimal()
        Me.ServiceDate = dataXml.SelectSingleNode("ServiceDate").AsDateTime()
        Me.AuthorizationNumber = dataXml.SelectSingleNode("AuthorizationNumber").AsString()
        Me.PerformsFunctionalUnitId = dataXml.SelectSingleNode("PerformsFunctionalUnitId").AsInt()
        Me.PerformsHealthProfessionalCode = dataXml.SelectSingleNode("PerformsHealthProfessionalCode").AsString()
        Me.PerformsProfessionalSpecialty = dataXml.SelectSingleNode("PerformsProfessionalSpecialty").AsString()
        Me.PerformsHealthProfessionalThirdPartyId = dataXml.SelectSingleNode("PerformsHealthProfessionalThirdPartyId").AsInt()
        Me.BillingConceptId = dataXml.SelectSingleNode("BillingConceptId").AsInt()
        Me.CostCenterId = dataXml.SelectSingleNode("CostCenterId").AsInt()
        Me.SettlementType = dataXml.SelectSingleNode("SettlementType").AsByte()
        Me.RateManualId = dataXml.SelectSingleNode("RateManualId").AsInt()
        Me.RateManualType = dataXml.SelectSingleNode("RateManualType").AsByte()
        Me.RateManualDetailId = dataXml.SelectSingleNode("RateManualDetailId").AsInt()
        Me.DefinitionRateDetailId = dataXml.SelectSingleNode("DefinitionRateDetailId").AsInt()
        Me.DefinitionRateDetailConditionId = dataXml.SelectSingleNode("DefinitionRateDetailConditionId").AsInt()
        Me.SubTotalSalesPrice = dataXml.SelectSingleNode("SubTotalSalesPrice").AsDecimal()
        Me.ThirdPartyDiscount = dataXml.SelectSingleNode("ThirdPartyDiscount").AsDecimal()
        Me.ThirdPartyDiscountPercentage = dataXml.SelectSingleNode("ThirdPartyDiscountPercentage").AsDecimal()
        Me.TotalSalesPrice = dataXml.SelectSingleNode("TotalSalesPrice").AsDecimal()
        Me.GrandTotalSalesPrice = dataXml.SelectSingleNode("GrandTotalSalesPrice").AsDecimal()
        Me.SurchargeApply = dataXml.SelectSingleNode("SurchargeApply").AsBoolean()
        Me.SurgicalInterventionType = dataXml.SelectSingleNode("SurgicalInterventionType").AsByte()
        Me.SurgeryNumber = dataXml.SelectSingleNode("SurgeryNumber").AsByte()
        Me.IsFirstEvent = dataXml.SelectSingleNode("IsFirstEvent").AsBoolean()
        Me.IsAnnulled = dataXml.SelectSingleNode("IsAnnulled").AsBoolean()
        Me.IsDelete = dataXml.SelectSingleNode("IsDelete").AsBoolean()
        Me.IncomeMainAccountId = dataXml.SelectSingleNode("IncomeMainAccountId").AsInt()
        Me.CodeNameFunctionalUnit = dataXml.SelectSingleNode("CodeNameFunctionalUnit").AsString()
        Me.PreviusServiceOrderDetailId = dataXml.SelectSingleNode("PreviusServiceOrderDetailId").AsInt()
        Me.CodeNameCareGroup = dataXml.SelectSingleNode("CodeNameCareGroup").AsString()
        Me.CodeNameCostCenter = dataXml.SelectSingleNode("CodeNameCostCenter").AsString()
        Me.CodeNameCups = dataXml.SelectSingleNode("CodeNameCups").AsString()
        Me.CodeNameIpsService = dataXml.SelectSingleNode("CodeNameIpsService").AsString()
        Me.IsSOAT = dataXml.SelectSingleNode("IsSOAT").AsBoolean()

        If dataXml.SelectNodes("ServiceOrderDetailSurgicalXml/ServiceOrderDetailSurgical").Count > 0 Then
            Me.ServiceOrderDetailSurgical = New TrackableCollection(Of ServiceOrderDetailSurgical)()
            For Each s As Xml.XmlNode In dataXml.SelectNodes("ServiceOrderDetailSurgicalXml/ServiceOrderDetailSurgical")
                Me.ServiceOrderDetailSurgical.Add(New Entities.ServiceOrderDetailSurgical(s))
            Next
        End If

    End Sub

End Class
