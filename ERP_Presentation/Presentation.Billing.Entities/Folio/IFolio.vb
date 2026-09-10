Public Interface IFolio

    Property RevenueControlDetailId As Integer

    Property AdmissionNumber As String

    Property BillingAuthorizationId As Integer?

    Property ThirdPartyId As Integer

    Property ThirdPartyNitName As String

    Property CareGroupId As Integer

    Property CaregroupEntityType As Byte

    Property CareGroupCodeName As String

    Property CareGroupCostCenterId As Integer

    Property FolioType As Byte

    Property Observation As String

    Property TotalFolio As String

    Property ResponsibleRecoveryFee As Byte

    Property PatientDiscountPercentage As Decimal

    Property PatientDiscount As Decimal

    Property TotalPatientSalesPrice As Decimal

    Property TotalPatientWithDiscount As Decimal

    Property VoucherValue As Decimal

    Property Status As Byte

    Property StatusFolioId As Integer?

    Property LiquidationType As Byte

    Property FolioOrder As Byte

    Property ContractId As Integer?

    Property ContractEntityCodeName As String

    Property InvoiceCategoryId As Integer?

    Property PatientCode As String

    Property ContractEntityId As Integer?

    Property ContractCodeName As String

    Property InvoiceCategoryCodeName As String

    Property AdmissionNumberPatient As String

    Property ThirdPartyPatientId As Integer

    Property HealthAdministratorId As Integer?

    Property HealthAdministratorCodeName As String

    Property ThirdPartyHealthAdministrator As Integer?

    Property InvoiceId As Integer?

    Property InvoiceNumber As String

    Property InvoiceDate As DateTime

    Property InvoicedUser As String

    Property StatusFolioName As String

    Property IsMasterAccount As Byte

    Property CurrencyAbbreviation As String

    Property ThirdPartyResponsibleQuotaNitName As String
    Property PatientQuotaResponsibleThirdPartyId As Integer?
    Property Details As IEnumerable(Of IFolioDetail)

    ''' <summary>
    ''' Id Sin recaudo de cuota
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property FeeNotCollectedId As Integer?

    ''' <summary>
    ''' Id de la factura basica 
    ''' </summary>
    ''' <returns></returns>
    Property BasicbillingCopayId As Integer?

    ''' <summary>
    ''' Aplica o no para logica tercero beneficiario
    ''' </summary>
    ''' <returns></returns>
    Property ApplyLogicThirdPartyBeneficiary As Boolean
End Interface
