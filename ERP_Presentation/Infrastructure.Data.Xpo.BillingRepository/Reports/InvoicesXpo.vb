Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Billing.Invoice")> _
Public Class InvoicesXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)> _
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

    Dim fDocumentType As Byte
    Public Property DocumentType() As Byte
        Get
            Return fDocumentType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("DocumentType", fDocumentType, value)
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

    Dim fInvoiceNumber As String
    <Size(15)> _
    Public Property InvoiceNumber() As String
        Get
            Return fInvoiceNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoiceNumber", fInvoiceNumber, value)
        End Set
    End Property

    Dim fRevenueControlDetailId As Integer
    Public Property RevenueControlDetailId() As Integer
        Get
            Return fRevenueControlDetailId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RevenueControlDetailId", fRevenueControlDetailId, value)
        End Set
    End Property

    Dim fAdmissionNumber As String
    <Size(10)> _
    Public Property AdmissionNumber() As String
        Get
            Return fAdmissionNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AdmissionNumber", fAdmissionNumber, value)
        End Set
    End Property

    Dim fHealthAdministratorId As AdministratorHealthXpo
    <Association("Billing_InvoiceReferencesContract_HealthAdministrator")> _
    Public Property HealthAdministratorId() As AdministratorHealthXpo
        Get
            Return fHealthAdministratorId
        End Get
        Set(ByVal value As AdministratorHealthXpo)
            SetPropertyValue(Of AdministratorHealthXpo)("HealthAdministratorId", fHealthAdministratorId, value)
        End Set
    End Property

    Dim fThirdPartyId As ThirdsPartyXpo
    <Association("Billing_InvoiceReferencesCommon_ThirdParty")> _
    Public Property ThirdPartyId() As ThirdsPartyXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As ThirdsPartyXpo)
            SetPropertyValue(Of ThirdsPartyXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property

    Dim fPatientCode As String
    <Size(15)> _
    Public Property PatientCode() As String
        Get
            Return fPatientCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientCode", fPatientCode, value)
        End Set
    End Property

    Dim fCareGroupId As CareGroupReportXpo
    <Association("Billing_InvoiceReferencesContract_CareGroup")> _
    Public Property CareGroupId() As CareGroupReportXpo
        Get
            Return fCareGroupId
        End Get
        Set(ByVal value As CareGroupReportXpo)
            SetPropertyValue(Of CareGroupReportXpo)("CareGroupId", fCareGroupId, value)
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

    Dim fOutputDate As DateTime
    Public Property OutputDate() As DateTime
        Get
            Return fOutputDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("OutputDate", fOutputDate, value)
        End Set
    End Property

    Dim fInitialDate As DateTime
    Public Property InitialDate() As DateTime
        Get
            Return fInitialDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InitialDate", fInitialDate, value)
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

    Dim fTotalInvoice As Decimal
    Public Property TotalInvoice() As Decimal
        Get
            Return fTotalInvoice
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalInvoice", fTotalInvoice, value)
        End Set
    End Property

    Dim fCapitationInitialDate As DateTime
    Public Property CapitationInitialDate() As DateTime
        Get
            Return fCapitationInitialDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CapitationInitialDate", fCapitationInitialDate, value)
        End Set
    End Property

    Dim fCapitationEndDate As DateTime
    Public Property CapitationEndDate() As DateTime
        Get
            Return fCapitationEndDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CapitationEndDate", fCapitationEndDate, value)
        End Set
    End Property

    Dim fCapitationlPatientsAmount As Integer
    Public Property CapitationlPatientsAmount() As Integer
        Get
            Return fCapitationlPatientsAmount
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CapitationlPatientsAmount", fCapitationlPatientsAmount, value)
        End Set
    End Property

    Dim fCapitationPatientValue As Decimal
    Public Property CapitationPatientValue() As Decimal
        Get
            Return fCapitationPatientValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CapitationPatientValue", fCapitationPatientValue, value)
        End Set
    End Property

    Dim fCashReceiptId As Integer
    Public Property CashReceiptId() As Integer
        Get
            Return fCashReceiptId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CashReceiptId", fCashReceiptId, value)
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

    Dim fJournalVoucherId As Integer
    Public Property JournalVoucherId() As Integer
        Get
            Return fJournalVoucherId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("JournalVoucherId", fJournalVoucherId, value)
        End Set
    End Property

    Dim fPatientPaidValue As Decimal
    Public Property PatientPaidValue() As Decimal
        Get
            Return fPatientPaidValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PatientPaidValue", fPatientPaidValue, value)
        End Set
    End Property

    Dim fThirdPartyAccountReceivableValue As Decimal
    Public Property ThirdPartyAccountReceivableValue() As Decimal
        Get
            Return fThirdPartyAccountReceivableValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ThirdPartyAccountReceivableValue", fThirdPartyAccountReceivableValue, value)
        End Set
    End Property

    Dim fPatientAccountReceivableValue As Decimal
    Public Property PatientAccountReceivableValue() As Decimal
        Get
            Return fPatientAccountReceivableValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PatientAccountReceivableValue", fPatientAccountReceivableValue, value)
        End Set
    End Property

    Dim fPatientAccountReceivableId As Integer
    Public Property PatientAccountReceivableId() As Integer
        Get
            Return fPatientAccountReceivableId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PatientAccountReceivableId", fPatientAccountReceivableId, value)
        End Set
    End Property

    'Dim fReversalTypeId As Integer
    'Public Property ReversalTypeId() As Integer
    '    Get
    '        Return fReversalTypeId
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("ReversalTypeId", fReversalTypeId, value)
    '    End Set
    'End Property

    'Dim fReversalReason As String
    '<Size(300)> _
    'Public Property ReversalReason() As String
    '    Get
    '        Return fReversalReason
    '    End Get
    '    Set(ByVal value As String)
    '        SetPropertyValue(Of String)("ReversalReason", fReversalReason, value)
    '    End Set
    'End Property

    Dim fPatientType As Integer
    Public Property PatientType() As Integer
        Get
            Return fPatientType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PatientType", fPatientType, value)
        End Set
    End Property

    Dim fPatientAffiliatedType As Integer
    Public Property PatientAffiliatedType() As Integer
        Get
            Return fPatientAffiliatedType
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PatientAffiliatedType", fPatientAffiliatedType, value)
        End Set
    End Property

    Dim fPatientPaidAbility As Integer
    Public Property PatientPaidAbility() As Integer
        Get
            Return fPatientPaidAbility
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PatientPaidAbility", fPatientPaidAbility, value)
        End Set
    End Property

    Dim fPatientSocialClass As String
    <Size(2)> _
    Public Property PatientSocialClass() As String
        Get
            Return fPatientSocialClass
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PatientSocialClass", fPatientSocialClass, value)
        End Set
    End Property

    Dim fCREETaxRetentionValue As Decimal
    Public Property CREETaxRetentionValue() As Decimal
        Get
            Return fCREETaxRetentionValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CREETaxRetentionValue", fCREETaxRetentionValue, value)
        End Set
    End Property

    Dim fCREETaxRetentionBaseValue As Decimal
    Public Property CREETaxRetentionBaseValue() As Decimal
        Get
            Return fCREETaxRetentionBaseValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CREETaxRetentionBaseValue", fCREETaxRetentionBaseValue, value)
        End Set
    End Property

    Dim fInvoiceCategoryId As InvoiceCategoriesReportXpo
    <Association("Billing_InvoiceReferencesBilling_InvoiceCategories")> _
    Public Property InvoiceCategoryId() As InvoiceCategoriesReportXpo
        Get
            Return fInvoiceCategoryId
        End Get
        Set(ByVal value As InvoiceCategoriesReportXpo)
            SetPropertyValue(Of InvoiceCategoriesReportXpo)("InvoiceCategoryId", fInvoiceCategoryId, value)
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

    Dim fInvoicedUser As String
    <Size(20)> _
    Public Property InvoicedUser() As String
        Get
            Return fInvoicedUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("InvoicedUser", fInvoicedUser, value)
        End Set
    End Property

    Dim fInvoicedDate As DateTime
    Public Property InvoicedDate() As DateTime
        Get
            Return fInvoicedDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InvoicedDate", fInvoicedDate, value)
        End Set
    End Property

    Dim fAnnulmentUser As String
    <Size(20)> _
    Public Property AnnulmentUser() As String
        Get
            Return fAnnulmentUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AnnulmentUser", fAnnulmentUser, value)
        End Set
    End Property

    Dim fAnnulmentDate As DateTime
    Public Property AnnulmentDate() As DateTime
        Get
            Return fAnnulmentDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AnnulmentDate", fAnnulmentDate, value)
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

    Dim fBillingAuthorizationId As Integer
    Public Property BillingAuthorizationId() As Integer
        Get
            Return fBillingAuthorizationId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("BillingAuthorizationId", fBillingAuthorizationId, value)
        End Set
    End Property

    Dim fContractId As ContractsXpo
    <Association("Billing_Invoice_References_Contract_Contract")>
    Public Property ContractId() As ContractsXpo
        Get
            Return fContractId
        End Get
        Set(ByVal value As ContractsXpo)
            SetPropertyValue(Of ContractsXpo)("ContractId", fContractId, value)
        End Set
    End Property

    Dim fCurrencyId As CommonCurrencyXpo
    <Association("Billing_Invoice_References_Common_Currency")>
    Public Property CurrencyId() As CommonCurrencyXpo
        Get
            Return fCurrencyId
        End Get
        Set(ByVal value As CommonCurrencyXpo)
            SetPropertyValue(Of CommonCurrencyXpo)("CurrencyId", fCurrencyId, value)
        End Set
    End Property

    Dim fTRMValue As Nullable(Of Decimal)
    Public Property TRMValue() As Nullable(Of Decimal)
        Get
            Return fTRMValue
        End Get
        Set(ByVal value As Nullable(Of Decimal))
            SetPropertyValue(Of Nullable(Of Decimal))("TRMValue", fTRMValue, value)
        End Set
    End Property

#End Region

#Region "Navigations"

    <Association("Billing_InvoiceDetailReferencesBilling_Invoice", GetType(DetailInvoicesXpo))>
    Public ReadOnly Property Billing_InvoiceDetails() As XPCollection(Of DetailInvoicesXpo)
        Get
            Return GetCollection(Of DetailInvoicesXpo)("Billing_InvoiceDetails")
        End Get
    End Property
    'Se inhabilita asociación a factura monto fijo - PBI 5755
    '<Association("Billing_EntityCapitatedReferencesBilling_Invoice", GetType(InvoiceEntityCapitatedReportXpo))> _
    'Public ReadOnly Property Billing_EntityCapitated() As XPCollection(Of InvoiceEntityCapitatedReportXpo)
    '    Get
    '        Return GetCollection(Of InvoiceEntityCapitatedReportXpo)("Billing_EntityCapitated")
    '    End Get
    'End Property
    <Association("PortfolioAccountReceivableXpoReferencesInvoicesXpo", GetType(PortfolioAccountReceivableXpo))> _
    Public ReadOnly Property PortfolioAccountReceivableXpo() As XPCollection(Of PortfolioAccountReceivableXpo)
        Get
            Return GetCollection(Of PortfolioAccountReceivableXpo)("PortfolioAccountReceivableXpo")
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

#End Region

End Class
