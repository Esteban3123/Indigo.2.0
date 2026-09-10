Imports DevExpress.Xpo

<Persistent("Billing.VReportInvoicePartial")>
Public Class BillingVReportInvoicePartial
    Inherits XPLiteObject

#Region "Properties"

    Dim fId As Integer
    <Key>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
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

    Dim fCustomerNit As String
    Public Property CustomerNit() As String
        Get
            Return fCustomerNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CustomerNit", fCustomerNit, value)
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

    Dim fCustomerName As String
    Public Property CustomerName() As String
        Get
            Return fCustomerName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CustomerName", fCustomerName, value)
        End Set
    End Property

    Dim fCustomerAddress As String
    Public Property CustomerAddress() As String
        Get
            Return fCustomerAddress
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CustomerAddress", fCustomerAddress, value)
        End Set
    End Property

    Dim fCustomerPhone As String
    Public Property CustomerPhone() As String
        Get
            Return fCustomerPhone
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CustomerPhone", fCustomerPhone, value)
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

    Dim fPatientType As Integer
    Public Property PatientType() As Integer
        Get
            Return fPatientType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PatientType", fPatientType, value)
        End Set
    End Property

    Dim fAffiliateType As Integer
    Public Property AffiliateType() As Integer
        Get
            Return fAffiliateType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AffiliateType", fAffiliateType, value)
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

    Dim fAdmissionType As Integer
    Public Property AdmissionType() As Integer
        Get
            Return fAdmissionType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AdmissionType", fAdmissionType, value)
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

    Dim fEgressDate As DateTime?
    Public Property EgressDate() As DateTime?
        Get
            Return fEgressDate
        End Get
        Set(ByVal value As DateTime?)
            SetPropertyValue(Of DateTime?)("EgressDate", fEgressDate, value)
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

    Dim fProcessingDate As DateTime
    Public Property ProcessingDate() As DateTime
        Get
            Return fProcessingDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of String)("ProcessingDate", fProcessingDate, value)
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

    Dim fDocumentType As Byte
    Public Property DocumentType() As Byte
        Get
            Return fDocumentType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("DocumentType", fDocumentType, value)
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

    Dim fTotalThirdPartyPortfolioAdvance As Decimal
    Public Property TotalThirdPartyPortfolioAdvance() As Decimal
        Get
            Return fTotalThirdPartyPortfolioAdvance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalThirdPartyPortfolioAdvance", fTotalThirdPartyPortfolioAdvance, value)
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

    Dim fUserFullName As String
    Public Property UserFullName() As String
        Get
            Return fUserFullName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserFullName", fUserFullName, value)
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

    Dim fTerm As Integer?
    Public Property Term() As Integer?
        Get
            Return fTerm
        End Get
        Set(ByVal value As Integer?)
            SetPropertyValue(Of Integer?)("Term", fTerm, value)
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
#End Region

#Region "Navigations Properties"

    <Association("VReportInvoicePartial_References_VReportInvoicePartialDetail", GetType(BillingVReportInvoicePartialDetail))>
    Public ReadOnly Property BillingVReportInvoicePartialDetail() As XPCollection(Of BillingVReportInvoicePartialDetail)
        Get
            Return GetCollection(Of BillingVReportInvoicePartialDetail)("BillingVReportInvoicePartialDetail")
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
