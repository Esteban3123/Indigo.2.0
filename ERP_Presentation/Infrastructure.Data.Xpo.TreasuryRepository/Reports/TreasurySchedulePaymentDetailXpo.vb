Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports Infrastructure.CrossCutting.Base

<Persistent("Treasury.SchedulePaymentDetail")> _
Public Class TreasurySchedulePaymentDetailXpo
    Inherits XPLiteObject
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
    Dim fSupplierId As CommonSuplierReportXpo
    <Association("TreasurySchedulePaymentDetailXpoReferencesCommonSuplierReportXpo")> _
    Public Property SupplierId() As CommonSuplierReportXpo
        Get
            Return fSupplierId
        End Get
        Set(ByVal value As CommonSuplierReportXpo)
            SetPropertyValue(Of CommonSuplierReportXpo)("SupplierId", fSupplierId, value)
        End Set
    End Property
    Dim fThirdPartyId As CommonThirdPartyReportXpo
    <Association("TreasurySchedulePaymentDetailXpoReferencesCommonThirdPartyReportXpo")>
    Public Property ThirdPartyId() As CommonThirdPartyReportXpo
        Get
            Return fThirdPartyId
        End Get
        Set(ByVal value As CommonThirdPartyReportXpo)
            SetPropertyValue(Of CommonThirdPartyReportXpo)("ThirdPartyId", fThirdPartyId, value)
        End Set
    End Property

    Dim fSchedulePaymentId As TreasurySchedulePaymentXpo
    <Association("TreasurySchedulePaymentDetailXpoReferencesTreasurySchedulePaymentXpo")>
    Public Property SchedulePaymentId() As TreasurySchedulePaymentXpo
        Get
            Return fSchedulePaymentId
        End Get
        Set(ByVal value As TreasurySchedulePaymentXpo)
            SetPropertyValue(Of TreasurySchedulePaymentXpo)("SchedulePaymentId", fSchedulePaymentId, value)
        End Set
    End Property

    <PersistentAlias("SchedulePaymentId.Code")>
    Public ReadOnly Property Code() As String
        Get
            Return Convert.ToString(EvaluateAlias("Code"))
        End Get
    End Property

    <PersistentAlias("SchedulePaymentId.ScheduledDate")>
    Public ReadOnly Property ScheduledDate() As DateTime?
        Get
            Return Convert.ToDateTime(EvaluateAlias("ScheduledDate"))
        End Get
    End Property

    Dim fDistributionLineId As DistributionLinesXpo
    <Association("Treasury_SchedulePaymentDetailXpo_References_Treasury_DistributionLinesXpo")>
    Public Property DistributionLineId() As DistributionLinesXpo
        Get
            Return fDistributionLineId
        End Get
        Set(ByVal value As DistributionLinesXpo)
            SetPropertyValue(Of DistributionLinesXpo)("DistributionLineId", fDistributionLineId, value)
        End Set
    End Property
    Dim fExpenseConceptId As TreasuryExpenseConceptsXpo
    <Association("TreasurySchedulePaymentDetailXpoReferencesTreasuryExpenseConceptsXpo")> _
    Public Property ExpenseConceptId() As TreasuryExpenseConceptsXpo
        Get
            Return fExpenseConceptId
        End Get
        Set(ByVal value As TreasuryExpenseConceptsXpo)
            SetPropertyValue(Of TreasuryExpenseConceptsXpo)("ExpenseConceptId", fExpenseConceptId, value)
        End Set
    End Property
    Dim fMainAccountId As GeneralLedgerMainAccountsXpo
    <Association("TreasurySchedulePaymentDetailXpoReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property MainAccountId() As GeneralLedgerMainAccountsXpo
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("MainAccountId", fMainAccountId, value)
        End Set
    End Property
    Dim fNature As Byte
    Public Property Nature() As Byte
        Get
            Return fNature
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("Nature", fNature, value)
        End Set
    End Property
    Dim fAccountPayableId As PaymentsAccountPayableXpo
    <Association("TreasurySchedulePaymentDetailXpoReferencesPaymentsAccountPayableXpo")> _
    Public Property AccountPayableId() As PaymentsAccountPayableXpo
        Get
            Return fAccountPayableId
        End Get
        Set(ByVal value As PaymentsAccountPayableXpo)
            SetPropertyValue(Of PaymentsAccountPayableXpo)("AccountPayableId", fAccountPayableId, value)
        End Set
    End Property
    Dim fAccountPayableShareId As TreasuryAccountPayableSharesXpo
    <Association("TreasurySchedulePaymentDetailXpoReferencesTreasuryAccountPayableSharesXpo")> _
    Public Property AccountPayableShareId() As TreasuryAccountPayableSharesXpo
        Get
            Return fAccountPayableShareId
        End Get
        Set(ByVal value As TreasuryAccountPayableSharesXpo)
            SetPropertyValue(Of TreasuryAccountPayableSharesXpo)("AccountPayableShareId", fAccountPayableShareId, value)
        End Set
    End Property
    Dim fAmountPaid As Decimal
    Public Property AmountPaid() As Decimal
        Get
            Return fAmountPaid
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AmountPaid", fAmountPaid, value)
        End Set
    End Property
    Dim fAmountPercent As Decimal
    Public Property AmountPercent() As Decimal
        Get
            Return fAmountPercent
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AmountPercent", fAmountPercent, value)
        End Set
    End Property
    Dim fPaymentConceptId As TreasuryPaymentConceptsXpo
    <Association("TreasurySchedulePaymentDetailXpoReferencesTreasuryPaymentConceptsXpo")> _
    Public Property PaymentConceptId() As TreasuryPaymentConceptsXpo
        Get
            Return fPaymentConceptId
        End Get
        Set(ByVal value As TreasuryPaymentConceptsXpo)
            SetPropertyValue(Of TreasuryPaymentConceptsXpo)("PaymentConceptId", fPaymentConceptId, value)
        End Set
    End Property
    Dim fPaid As Boolean
    Public Property Paid() As Boolean
        Get
            Return fPaid
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Paid", fPaid, value)
        End Set
    End Property
    Dim fDescription As String
    <Size(300)> _
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Description", fDescription, value)
        End Set
    End Property
    Dim fVoucherTransactionId As TreasuryVoucherTransactionXpo
    <Association("TreasurySchedulePaymentDetailXpoReferencesTreasuryVoucherTransactionXpo")> _
    Public Property VoucherTransactionId() As TreasuryVoucherTransactionXpo
        Get
            Return fVoucherTransactionId
        End Get
        Set(ByVal value As TreasuryVoucherTransactionXpo)
            SetPropertyValue(Of TreasuryVoucherTransactionXpo)("VoucherTransactionId", fVoucherTransactionId, value)
        End Set
    End Property
    Dim fGeneratedVoucher As Boolean
    Public Property GeneratedVoucher() As Boolean
        Get
            Return fGeneratedVoucher
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("GeneratedVoucher", fGeneratedVoucher, value)
        End Set
    End Property

    <PersistentAlias("AccountPayableId.CurrencyAbbreviation")>
    Public ReadOnly Property CurrencyAbbreviation() As String
        Get
            Return If(String.IsNullOrEmpty(Convert.ToString(EvaluateAlias("CurrencyAbbreviation"))),
                        SessionValues.Instance.CurrencyISO4217, Convert.ToString(EvaluateAlias("CurrencyAbbreviation")))
        End Get
    End Property

    <PersistentAlias("AccountPayableId.CurrencyId")>
    Public ReadOnly Property CurrencyId() As Integer?
        Get
            Return Convert.ToInt32(EvaluateAlias("CurrencyId"))
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
