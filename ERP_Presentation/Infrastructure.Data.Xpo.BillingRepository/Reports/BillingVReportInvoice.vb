Imports DevExpress.Xpo

<Persistent("Billing.VReportInvoice")>
Public Class BillingVReportInvoice
    Inherits XPLiteObject

#Region "Properties"

    Dim fId As Integer
    <Key()>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property

    Dim fOperatingUnitId As Integer
    Public Property OperatingUnitId() As Integer
        Get
            Return fOperatingUnitId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OperatingUnitId", fOperatingUnitId, value)
        End Set
    End Property

    Dim fInvoiceDate As DateTime
    Public Property InvoiceDate() As DateTime
        Get
            Return fInvoiceDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InvoiceDate", fInvoiceDate, value)
        End Set
    End Property

    Dim fInvoiceExpirationDate As DateTime
    Public Property InvoiceExpirationDate() As DateTime
        Get
            Return fInvoiceExpirationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InvoiceExpirationDate", fInvoiceExpirationDate, value)
        End Set
    End Property

    Dim fInvoiceNumber As String
    Public Property InvoiceNumber() As String
        Get
            Return fInvoiceNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoiceNumber", fInvoiceNumber, value)
        End Set
    End Property

    Dim fElectronicInvoiceNumber As String
    Public Property ElectronicInvoiceNumber() As String
        Get
            Return fElectronicInvoiceNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ElectronicInvoiceNumber", fElectronicInvoiceNumber, value)
        End Set
    End Property

    Dim fCutType As Byte
    Public Property CutType() As Byte
        Get
            Return fCutType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("CutType", fCutType, value)
        End Set
    End Property

    Dim fNit As String
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
        End Set
    End Property

    Dim fName As String
    Public Property Name() As String
        Get
            Return fName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Name", fName, value)
        End Set
    End Property

    Dim fThirdPartyAddress As String
    Public Property ThirdPartyAddress() As String
        Get
            Return fThirdPartyAddress
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyAddress", fThirdPartyAddress, value)
        End Set
    End Property

    Dim fThirdPartyPhone As String
    Public Property ThirdPartyPhone() As String
        Get
            Return fThirdPartyPhone
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyPhone", fThirdPartyPhone, value)
        End Set
    End Property

    Dim fCareGroup As String
    Public Property CareGroup() As String
        Get
            Return fCareGroup
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CareGroup", fCareGroup, value)
        End Set
    End Property

    Dim fCareGroupType As Byte
    Public Property CareGroupType() As Byte
        Get
            Return fCareGroupType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("CareGroupType", fCareGroupType, value)
        End Set
    End Property

    Dim fHealthEntityCode As String
    Public Property HealthEntityCode() As String
        Get
            Return fHealthEntityCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("HealthEntityCode", fHealthEntityCode, value)
        End Set
    End Property

    Dim fDescriptionHealthAdministrator As String
    Public Property DescriptionHealthAdministrator() As String
        Get
            Return fDescriptionHealthAdministrator
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DescriptionHealthAdministrator", fDescriptionHealthAdministrator, value)
        End Set
    End Property

    Dim fContract As String
    Public Property Contract() As String
        Get
            Return fContract
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Contract", fContract, value)
        End Set
    End Property

    Dim fPrintingMode As Byte?
    Public Property PrintingMode() As Byte?
        Get
            Return fPrintingMode
        End Get
        Set(ByVal value As Byte?)
            SetPropertyValue(Of Byte?)("PrintingMode", fPrintingMode, value)
        End Set
    End Property

    Dim fContractDate As DateTime?
    Public Property ContractDate As DateTime?
        Get
            Return fContractDate
        End Get
        Set(value As DateTime?)
            SetPropertyValue(Of DateTime?)("ContractDate", fContractDate, value)
        End Set
    End Property

    Dim fContractInitialDate As DateTime?
    Public Property ContractInitialDate As DateTime?
        Get
            Return fContractInitialDate
        End Get
        Set(value As DateTime?)
            SetPropertyValue(Of DateTime?)("ContractInitialDate", fContractInitialDate, value)
        End Set
    End Property

    Dim fContractEndDate As DateTime?
    Public Property ContractEndDate As DateTime?
        Get
            Return fContractEndDate
        End Get
        Set(value As DateTime?)
            SetPropertyValue(Of DateTime?)("ContractEndDate", fContractEndDate, value)
        End Set
    End Property

    Dim fCapitationInitialDate As DateTime?
    Public Property CapitationInitialDate As DateTime?
        Get
            Return fCapitationInitialDate
        End Get
        Set(value As DateTime?)
            SetPropertyValue(Of DateTime?)("CapitationInitialDate", fCapitationInitialDate, value)
        End Set
    End Property

    Dim fCapitationEndDate As DateTime?
    Public Property CapitationEndDate As DateTime?
        Get
            Return fCapitationEndDate
        End Get
        Set(value As DateTime?)
            SetPropertyValue(Of DateTime?)("CapitationEndDate", fCapitationEndDate, value)
        End Set
    End Property

    Dim fCapitationlPatientsAmount As Integer?
    Public Property CapitationlPatientsAmount As Integer?
        Get
            Return fCapitationlPatientsAmount
        End Get
        Set(value As Integer?)
            SetPropertyValue(Of Integer?)("CapitationlPatientsAmount", fCapitationlPatientsAmount, value)
        End Set
    End Property

    Dim fCapitationPatientValue As Decimal
    Public Property CapitationPatientValue As Decimal
        Get
            Return fCapitationPatientValue
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal?)("CapitationPatientValue", fCapitationPatientValue, value)
        End Set
    End Property

    Dim fTotalValue As Decimal
    Public Property TotalValue As Decimal
        Get
            Return fTotalValue
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal?)("TotalValue", fTotalValue, value)
        End Set
    End Property

    Dim fTotalInvoice As Decimal
    Public Property TotalInvoice As Decimal
        Get
            Return fTotalInvoice
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal?)("TotalInvoice", fTotalInvoice, value)
        End Set
    End Property
    Dim fPatientCode As String
    Public Property PatientCode() As String
        Get
            Return fPatientCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientCode", fPatientCode, value)
        End Set
    End Property

    Dim fPatientName As String
    Public Property PatientName() As String
        Get
            Return fPatientName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientName", fPatientName, value)
        End Set
    End Property

    Dim fPatientFirstName As String
    Public Property PatientFirstName() As String
        Get
            Return fPatientFirstName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientFirstName", fPatientFirstName, value)
        End Set
    End Property

    Dim fPatientSecondName As String
    Public Property PatientSecondName() As String
        Get
            Return fPatientSecondName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientSecondName", fPatientSecondName, value)
        End Set
    End Property

    Dim fPatientFirstLastName As String
    Public Property PatientFirstLastName() As String
        Get
            Return fPatientFirstLastName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientFirstLastName", fPatientFirstLastName, value)
        End Set
    End Property


    Dim fPatientSecondLastName As String
    Public Property PatientSecondLastName() As String
        Get
            Return fPatientSecondLastName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientSecondLastName", fPatientSecondLastName, value)
        End Set
    End Property

    Dim fPatientType As String
    Public Property PatientType() As String
        Get
            Return fPatientType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientType", fPatientType, value)
        End Set
    End Property

    Dim fAffiliateType As Integer?
    Public Property AffiliateType() As Integer?
        Get
            Return fAffiliateType
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("AffiliateType", fAffiliateType, value)
        End Set
    End Property

    Dim fPatientLevel As String
    Public Property PatientLevel() As String
        Get
            Return fPatientLevel
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientLevel", fPatientLevel, value)
        End Set
    End Property

    Dim fPatientAddress As String
    Public Property PatientAddress() As String
        Get
            Return fPatientAddress
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientAddress", fPatientAddress, value)
        End Set
    End Property

    Dim fPatientTelephoneNumber As String
    Public Property PatientTelephoneNumber() As String
        Get
            Return fPatientTelephoneNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientTelephoneNumber", fPatientTelephoneNumber, value)
        End Set
    End Property

    Dim fPatientPhoneMovil As String
    Public Property PatientPhoneMovil() As String
        Get
            Return fPatientPhoneMovil
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientPhoneMovil", fPatientPhoneMovil, value)
        End Set
    End Property

    Dim fPatientEmail As String
    Public Property PatientEmail() As String
        Get
            Return fPatientEmail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientEmail", fPatientEmail, value)
        End Set
    End Property


    Dim fPatientAge As String
    Public Property PatientAge() As String
        Get
            Return fPatientAge
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientAge", fPatientAge, value)
        End Set
    End Property

    Dim fTypeAdmission As Integer?
    Public Property TypeAdmission() As Integer?
        Get
            Return fTypeAdmission
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("TypeAdmission", fTypeAdmission, value)
        End Set
    End Property

    Dim fAdmissionNumber As String
    Public Property AdmissionNumber() As String
        Get
            Return fAdmissionNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionNumber", fAdmissionNumber, value)
        End Set
    End Property

    Dim fAdmissionDate As DateTime
    Public Property AdmissionDate() As DateTime
        Get
            Return fAdmissionDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AdmissionDate", fAdmissionDate, value)
        End Set
    End Property

    Dim fEgressDate As DateTime
    Public Property EgressDate() As DateTime
        Get
            Return fEgressDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("EgressDate", fEgressDate, value)
        End Set
    End Property

    Dim fAuthorizationNumber As String
    Public Property AuthorizationNumber() As String
        Get
            Return fAuthorizationNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AuthorizationNumber", fAuthorizationNumber, value)
        End Set
    End Property

    Dim fProcessingDate As DateTime?
    Public Property ProcessingDate() As DateTime?
        Get
            Return fProcessingDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("ProcessingDate", fProcessingDate, value)
        End Set
    End Property

    Dim fProcessLine As String
    Public Property ProcessLine() As String
        Get
            Return fProcessLine
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProcessLine", fProcessLine, value)
        End Set
    End Property

    Dim fPacientEntity As String
    Public Property PacientEntity() As String
        Get
            Return fPacientEntity
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PacientEntity", fPacientEntity, value)
        End Set
    End Property

    Dim fPacientRegimen As String
    Public Property PacientRegimen() As String
        Get
            Return fPacientRegimen
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PacientRegimen", fPacientRegimen, value)
        End Set
    End Property

    Dim fAffiliateStatus As String
    Public Property AffiliateStatus() As String
        Get
            Return fAffiliateStatus
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AffiliateStatus", fAffiliateStatus, value)
        End Set
    End Property

    Dim fERPConfirm As String
    Public Property ERPConfirm() As String
        Get
            Return fERPConfirm
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ERPConfirm", fERPConfirm, value)
        End Set
    End Property

    Dim fIPSReport As String
    Public Property IPSReport() As String
        Get
            Return fIPSReport
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IPSReport", fIPSReport, value)
        End Set
    End Property

    Dim fCODCENATE As String
    Public Property CODCENATE() As String
        Get
            Return fCODCENATE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CODCENATE", fCODCENATE, value)
        End Set
    End Property

    Dim fCenterAttentionName As String
    Public Property CenterAttentionName() As String
        Get
            Return fCenterAttentionName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CenterAttentionName", fCenterAttentionName, value)
        End Set
    End Property

    Dim fCenterAttentionCode As String
    Public Property CenterAttentionCode() As String
        Get
            Return fCenterAttentionCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CenterAttentionCode", fCenterAttentionCode, value)
        End Set
    End Property

    Dim fCenterAttentionAddress As String
    Public Property CenterAttentionAddress() As String
        Get
            Return fCenterAttentionAddress
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CenterAttentionAddress", fCenterAttentionAddress, value)
        End Set
    End Property

    Dim fCenterAttentionPhone As String
    Public Property CenterAttentionPhone() As String
        Get
            Return fCenterAttentionPhone
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CenterAttentionPhone", fCenterAttentionPhone, value)
        End Set
    End Property

    Dim fUFUIGRMED As String
    Public Property UFUIGRMED() As String
        Get
            Return fUFUIGRMED
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UFUIGRMED", fUFUIGRMED, value)
        End Set
    End Property

    Dim fUFUEGRMED As String
    Public Property UFUEGRMED() As String
        Get
            Return fUFUEGRMED
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UFUEGRMED", fUFUEGRMED, value)
        End Set
    End Property

    Dim fInvoiceCategory As String
    Public Property InvoiceCategory() As String
        Get
            Return fInvoiceCategory
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoiceCategory", fInvoiceCategory, value)
        End Set
    End Property

    Dim fDocumentType As Byte
    Public Property DocumentType() As Byte
        Get
            Return fDocumentType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("DocumentType", fDocumentType, value)
        End Set
    End Property

    Dim fIdentificationTypeCode As String
    Public Property IdentificationTypeCode() As String
        Get
            Return fIdentificationTypeCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IdentificationTypeCode", fIdentificationTypeCode, value)
        End Set
    End Property

    Dim fOutputDiagnosis As String
    Public Property OutputDiagnosis() As String
        Get
            Return fOutputDiagnosis
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("OutputDiagnosis", fOutputDiagnosis, value)
        End Set
    End Property

    Dim fSubTotalService As Decimal?
    Public Property SubTotalService() As Decimal?
        Get
            Return fSubTotalService
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("SubTotalService", fSubTotalService, value)
        End Set
    End Property

    Dim fThirdPartyDiscountValue As Decimal
    Public Property ThirdPartyDiscountValue() As Decimal
        Get
            Return fThirdPartyDiscountValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ThirdPartyDiscountValue", fThirdPartyDiscountValue, value)
        End Set
    End Property

    Dim fTotalPatientSalesPrice As Decimal
    Public Property TotalPatientSalesPrice() As Decimal
        Get
            Return fTotalPatientSalesPrice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalPatientSalesPrice", fTotalPatientSalesPrice, value)
        End Set
    End Property

    Dim fPatientDiscount As Decimal
    Public Property PatientDiscount() As Decimal
        Get
            Return fPatientDiscount
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PatientDiscount", fPatientDiscount, value)
        End Set
    End Property

    Dim fTotalPatientAccountReceivable As Decimal?
    Public Property TotalPatientAccountReceivable() As Decimal?
        Get
            Return fTotalPatientAccountReceivable
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("TotalPatientAccountReceivable", fTotalPatientAccountReceivable, value)
        End Set
    End Property

    Dim fTotalThirdPartyPortfolioAdvance As Decimal?
    Public Property TotalThirdPartyPortfolioAdvance() As Decimal?
        Get
            Return fTotalThirdPartyPortfolioAdvance
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("TotalThirdPartyPortfolioAdvance", fTotalThirdPartyPortfolioAdvance, value)
        End Set
    End Property

    Dim fThirdPartySalesValue As Decimal
    Public Property ThirdPartySalesValue() As Decimal
        Get
            Return fThirdPartySalesValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ThirdPartySalesValue", fThirdPartySalesValue, value)
        End Set
    End Property

    Dim fUserCode As String
    Public Property UserCode() As String
        Get
            Return fUserCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserCode", fUserCode, value)
        End Set
    End Property

    Dim fFullNameUser As String
    Public Property FullNameUser() As String
        Get
            Return fFullNameUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FullNameUser", fFullNameUser, value)
        End Set
    End Property

    Dim fStatus As Byte
    Public Property Status() As Byte
        Get
            Return fStatus
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Status", fStatus, value)
        End Set
    End Property

    Dim fPolicyNumber As String
    Public Property PolicyNumber() As String
        Get
            Return fPolicyNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PolicyNumber", fPolicyNumber, value)
        End Set
    End Property

    Dim fPermanentObservationOfTheInvoice As String
    Public Property PermanentObservationOfTheInvoice() As String
        Get
            Return fPermanentObservationOfTheInvoice
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PermanentObservationOfTheInvoice", fPermanentObservationOfTheInvoice, value)
        End Set
    End Property

    Dim fIdPay As Integer?
    Public Property IdPay() As Integer?
        Get
            Return fIdPay
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("IdPay", fIdPay, value)
        End Set
    End Property

    Dim fResolutionNumber As String
    Public Property ResolutionNumber() As String
        Get
            Return fResolutionNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ResolutionNumber", fResolutionNumber, value)
        End Set
    End Property

    Dim fResolutionDate As Date?
    Public Property ResolutionDate() As Date?
        Get
            Return fResolutionDate
        End Get
        Set(ByVal value As Date?)
            SetPropertyValue(Of Date?)("ResolutionDate", fResolutionDate, value)
        End Set
    End Property

    Dim fResolutionInvoicePrefix As String
    Public Property ResolutionInvoicePrefix() As String
        Get
            Return fResolutionInvoicePrefix
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ResolutionInvoicePrefix", fResolutionInvoicePrefix, value)
        End Set
    End Property

    Dim fResolutionInitialInvoice As Long?
    Public Property ResolutionInitialInvoice() As Long?
        Get
            Return fResolutionInitialInvoice
        End Get
        Set(ByVal value As Long?)
            SetPropertyValue(Of Long?)("ResolutionInitialInvoice", fResolutionInitialInvoice, value)
        End Set
    End Property

    Dim fResolutionFinalInvoice As Long?
    Public Property ResolutionFinalInvoice() As Long?
        Get
            Return fResolutionFinalInvoice
        End Get
        Set(ByVal value As Long?)
            SetPropertyValue(Of Long?)("ResolutionFinalInvoice", fResolutionFinalInvoice, value)
        End Set
    End Property

    Dim fResolutionInitialDate As Date?
    Public Property ResolutionInitialDate() As Date?
        Get
            Return fResolutionInitialDate
        End Get
        Set(ByVal value As Date?)
            SetPropertyValue(Of Date?)("ResolutionInitialDate", fResolutionInitialDate, value)
        End Set
    End Property

    Dim fResolutionFinalDate As Date?
    Public Property ResolutionFinalDate() As Date?
        Get
            Return fResolutionFinalDate
        End Get
        Set(ByVal value As Date?)
            SetPropertyValue(Of Date?)("ResolutionFinalDate", fResolutionFinalDate, value)
        End Set
    End Property

    Dim fResolutionExpiration As Integer
    Public Property ResolutionExpiration() As Integer
        Get
            Return fResolutionExpiration
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)("ResolutionExpiration", fResolutionExpiration, value)
        End Set
    End Property

    Dim fTerm As Integer?
    Public Property Term() As Integer?
        Get
            Return fTerm
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("Term", fTerm, value)
        End Set
    End Property

    Dim fPaymentMeans As String
    Public Property PaymentMeans() As String
        Get
            Return fPaymentMeans
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PaymentMeans", fPaymentMeans, value)
        End Set
    End Property

    Dim fPaymentMethod As String
    Public Property PaymentMethod() As String
        Get
            Return fPaymentMethod
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PaymentMethod", fPaymentMethod, value)
        End Set
    End Property

    Dim fLiquidationType As Byte?
    Public Property LiquidationType() As Byte?
        Get
            Return fLiquidationType
        End Get
        Set(ByVal value As Byte?)
            SetPropertyValue(Of Byte?)("LiquidationType", fLiquidationType, value)
        End Set
    End Property

    Dim fCoverageDescription As String
    Public Property CoverageDescription() As String
        Get
            Return fCoverageDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CoverageDescription", fCoverageDescription, value)
        End Set
    End Property

    Dim fCUFE As String
    Public Property CUFE() As String
        Get
            Return fCUFE
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CUFE", fCUFE, value)
        End Set
    End Property

    Dim fQR As String
    Public Property QR() As String
        Get
            Return fQR
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("QR", fQR, value)
        End Set
    End Property

    Dim fStatusDIAN As String
    Public Property StatusDIAN() As String
        Get
            Return fStatusDIAN
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StatusDIAN", fStatusDIAN, value)
        End Set
    End Property

    Dim fValidationDate As DateTime?
    Public Property ValidationDate() As DateTime?
        Get
            Return fValidationDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("ValidationDate", fValidationDate, value)
        End Set
    End Property

    Dim fObservation As String
    Public Property Observation() As String
        Get
            Return fObservation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Observation", fObservation, value)
        End Set
    End Property

    Dim fCurrencyId As CommonCurrencyXpo
    Public Property CurrencyId() As CommonCurrencyXpo
        Get
            Return fCurrencyId
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue(Of CommonCurrencyXpo)("CurrencyId", fCurrencyId, value)
        End Set
    End Property

    Dim fCurrencyName As String
    Public Property CurrencyName() As String
        Get
            Return fCurrencyName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CurrencyName", fCurrencyName, value)
        End Set
    End Property

    Dim fCoinsuranceInsurance As Decimal?
    Public Property CoinsuranceInsurance() As Decimal?
        Get
            Return fCoinsuranceInsurance
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("CoinsuranceInsurance", fCoinsuranceInsurance, value)
        End Set
    End Property

    Dim fCopaymentValueInsurance As Decimal?
    Public Property CopaymentValueInsurance() As Decimal?
        Get
            Return fCopaymentValueInsurance
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("CopaymentValueInsurance", fCopaymentValueInsurance, value)
        End Set
    End Property

    Dim fDeductibleValueInsurance As Decimal?
    Public Property DeductibleValueInsurance() As Decimal?
        Get
            Return fDeductibleValueInsurance
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("DeductibleValueInsurance", fDeductibleValueInsurance, value)
        End Set
    End Property

    Dim fPatientCoinsurance As Decimal?
    Public Property PatientCoinsurance() As Decimal?
        Get
            Return fPatientCoinsurance
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("PatientCoinsurance", fPatientCoinsurance, value)
        End Set
    End Property

    Dim fPatientCopaymentValue As Decimal?
    Public Property PatientCopaymentValue() As Decimal?
        Get
            Return fPatientCopaymentValue
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("PatientCopaymentValue", fPatientCopaymentValue, value)
        End Set
    End Property

    Dim fPatientDeductibleValue As Decimal?
    Public Property PatientDeductibleValue() As Decimal?
        Get
            Return fPatientDeductibleValue
        End Get
        Set(ByVal value As Decimal?)
            SetPropertyValue(Of Decimal?)("PatientDeductibleValue", fPatientDeductibleValue, value)
        End Set
    End Property

    Dim fIsMasterAccount As Integer?
    Public Property IsMasterAccount() As Integer?
        Get
            Return fIsMasterAccount
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("IsMasterAccount", fIsMasterAccount, value)
        End Set
    End Property

    Dim fValueTax As Decimal
    Public Property ValueTax() As Decimal
        Get
            Return fValueTax
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueTax", fValueTax, value)
        End Set
    End Property

    Dim fTaxDevolutionValue As Decimal
    Public Property TaxDevolutionValue() As Decimal
        Get
            Return fTaxDevolutionValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TaxDevolutionValue", fTaxDevolutionValue, value)
        End Set
    End Property

    Dim fIdentificationName As String
    Public Property IdentificationName() As String
        Get
            Return fIdentificationName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IdentificationName", fIdentificationName, value)
        End Set
    End Property

    Dim fDataDiscountPercentage As Decimal
    Public Property DataDiscountPercentage As Decimal
        Get
            Return fDataDiscountPercentage
        End Get
        Set(value As Decimal)
            SetPropertyValue(Of Decimal)("DataDiscountPercentage", fDataDiscountPercentage, value)
        End Set
    End Property

    Dim fConditionSalesCodeName As String
    Public Property ConditionSalesCodeName As String
        Get
            Return fConditionSalesCodeName
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("ConditionSalesCodeName", fConditionSalesCodeName, value)
        End Set
    End Property

    Dim fNitWithOutDig As String
    Public Property NitWithOutDig() As String
        Get
            Return fNitWithOutDig
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NitWithOutDig", fNitWithOutDig, value)
        End Set
    End Property

    Dim fIsElectronicTicket As Boolean
    Public Property IsElectronicTicket() As Boolean
        Get
            Return fIsElectronicTicket
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsElectronicTicket", fIsElectronicTicket, value)
        End Set
    End Property

    <NonPersistent()>
    Public Property UserPrint() As String

    <NonPersistent()>
    Public Property SubtotalPatient() As String

    <NonPersistent()>
    Public Property SubtotalLetters() As String
#End Region

#Region "Navigations Properties"

    <Association("VReportInvoice_References_VReportInvoiceDetail", GetType(BillingVReportInvoiceDetail))>
    Public ReadOnly Property BillingVReportInvoiceDetail() As XPCollection(Of BillingVReportInvoiceDetail)
        Get
            Return GetCollection(Of BillingVReportInvoiceDetail)("BillingVReportInvoiceDetail")
        End Get
    End Property

    <Association("VReportInvoice_References_VReportInvoiceCustomerRetention", GetType(BillingVReportInvoiceCustomerRetention))>
    Public ReadOnly Property BillingVReportInvoiceCustomerRetention() As XPCollection(Of BillingVReportInvoiceCustomerRetention)
        Get
            Return GetCollection(Of BillingVReportInvoiceCustomerRetention)("BillingVReportInvoiceCustomerRetention")
        End Get
    End Property

    <Association("VReportInvoice_References_BasicBilling", GetType(BasicBillingReportXpo))>
    Public ReadOnly Property BasicBillingsReport() As XPCollection(Of BasicBillingReportXpo)
        Get
            Return GetCollection(Of BasicBillingReportXpo)("BasicBillingsReport")
        End Get
    End Property
    <Association("VReportInvoice_Reference_EntityCapitatedReferencesBilling", GetType(InvoiceEntityCapitatedReportXpo))>
    Public ReadOnly Property InvoiceEntityCapitatedReportXpo() As XPCollection(Of InvoiceEntityCapitatedReportXpo)
        Get
            Return GetCollection(Of InvoiceEntityCapitatedReportXpo)("InvoiceEntityCapitatedReportXpo")
        End Get
    End Property

    <Association("InvoiceCopay_References_Invoice", GetType(BillingInvoiceCopayXpo))>
    Public ReadOnly Property InvoiceCopays() As XPCollection(Of BillingInvoiceCopayXpo)
        Get
            Return GetCollection(Of BillingInvoiceCopayXpo)("InvoiceCopays")
        End Get
    End Property

#End Region

#Region "Builders"

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

#End Region

End Class
