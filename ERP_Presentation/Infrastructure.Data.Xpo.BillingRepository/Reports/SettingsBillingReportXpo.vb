Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Billing.SettingsBilling")> _
 Public Class SettingsBillingReportXpo
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
    Dim fIdOperatingUnit As Integer
    <Indexed(Name:="IX_SettingsBilling", Unique:=True)> _
    Public Property IdOperatingUnit() As Integer
        Get
            Return fIdOperatingUnit
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdOperatingUnit", fIdOperatingUnit, value)
        End Set
    End Property
    Dim fInvoiceJournalVoucherTypeId As Integer
    Public Property InvoiceJournalVoucherTypeId() As Integer
        Get
            Return fInvoiceJournalVoucherTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InvoiceJournalVoucherTypeId", fInvoiceJournalVoucherTypeId, value)
        End Set
    End Property
    Dim fInvoiceAnnulmentJournalVoucherTypeId As Integer
    Public Property InvoiceAnnulmentJournalVoucherTypeId() As Integer
        Get
            Return fInvoiceAnnulmentJournalVoucherTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("InvoiceAnnulmentJournalVoucherTypeId", fInvoiceAnnulmentJournalVoucherTypeId, value)
        End Set
    End Property
    Dim fReverseTransferJournalVoucherTypeId As Integer
    Public Property ReverseTransferJournalVoucherTypeId() As Integer
        Get
            Return fReverseTransferJournalVoucherTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ReverseTransferJournalVoucherTypeId", fReverseTransferJournalVoucherTypeId, value)
        End Set
    End Property
    Dim fProductInvoiceJournalVoucherTypeId As Integer
    Public Property ProductInvoiceJournalVoucherTypeId() As Integer
        Get
            Return fProductInvoiceJournalVoucherTypeId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProductInvoiceJournalVoucherTypeId", fProductInvoiceJournalVoucherTypeId, value)
        End Set
    End Property
    Dim fCashReceiptsConfirm As Boolean
    Public Property CashReceiptsConfirm() As Boolean
        Get
            Return fCashReceiptsConfirm
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("CashReceiptsConfirm", fCashReceiptsConfirm, value)
        End Set
    End Property
    Dim fRoundingTypeRecoveryFeeType As Byte
    Public Property RoundingTypeRecoveryFeeType() As Byte
        Get
            Return fRoundingTypeRecoveryFeeType
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("RoundingTypeRecoveryFeeType", fRoundingTypeRecoveryFeeType, value)
        End Set
    End Property
    Dim fCapitationRevenueMainAccountId As Integer
    Public Property CapitationRevenueMainAccountId() As Integer
        Get
            Return fCapitationRevenueMainAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CapitationRevenueMainAccountId", fCapitationRevenueMainAccountId, value)
        End Set
    End Property
    Dim fCapitationProfitMainAccountId As Integer
    Public Property CapitationProfitMainAccountId() As Integer
        Get
            Return fCapitationProfitMainAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CapitationProfitMainAccountId", fCapitationProfitMainAccountId, value)
        End Set
    End Property
    Dim fCapitationLossMainAccountId As Integer
    Public Property CapitationLossMainAccountId() As Integer
        Get
            Return fCapitationLossMainAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CapitationLossMainAccountId", fCapitationLossMainAccountId, value)
        End Set
    End Property
    Dim fPatientAdvanceCashReceiptConceptId As Integer
    Public Property PatientAdvanceCashReceiptConceptId() As Integer
        Get
            Return fPatientAdvanceCashReceiptConceptId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PatientAdvanceCashReceiptConceptId", fPatientAdvanceCashReceiptConceptId, value)
        End Set
    End Property
    Dim fIndvidualAdvanceCashReceiptConceptId As Integer
    Public Property IndvidualAdvanceCashReceiptConceptId() As Integer
        Get
            Return fIndvidualAdvanceCashReceiptConceptId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IndvidualAdvanceCashReceiptConceptId", fIndvidualAdvanceCashReceiptConceptId, value)
        End Set
    End Property
    Dim fPrefixConsecutiveCapitation As String
    <Size(4)> _
    Public Property PrefixConsecutiveCapitation() As String
        Get
            Return fPrefixConsecutiveCapitation
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PrefixConsecutiveCapitation", fPrefixConsecutiveCapitation, value)
        End Set
    End Property
    Dim fConsecutiveControlCapitation As Long
    Public Property ConsecutiveControlCapitation() As Long
        Get
            Return fConsecutiveControlCapitation
        End Get
        Set(ByVal value As Long)
            SetPropertyValue(Of Long)("ConsecutiveControlCapitation", fConsecutiveControlCapitation, value)
        End Set
    End Property
    Dim fRequiresPermissionForCxCPatient As Boolean
    Public Property RequiresPermissionForCxCPatient() As Boolean
        Get
            Return fRequiresPermissionForCxCPatient
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("RequiresPermissionForCxCPatient", fRequiresPermissionForCxCPatient, value)
        End Set
    End Property
    Dim fEntityCapitatedBillingAuthorizationId As BillingAuthorizationReportXpo
    <Association("Billing_SettingsBillingReferencesBilling_BillingAuthorization")> _
    Public Property EntityCapitatedBillingAuthorizationId() As BillingAuthorizationReportXpo
        Get
            Return fEntityCapitatedBillingAuthorizationId
        End Get
        Set(ByVal value As BillingAuthorizationReportXpo)
            SetPropertyValue(Of BillingAuthorizationReportXpo)("EntityCapitatedBillingAuthorizationId", fEntityCapitatedBillingAuthorizationId, value)
        End Set
    End Property
    Dim fAccountingForSurgical As Byte
    Public Property AccountingForSurgical() As Byte
        Get
            Return fAccountingForSurgical
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("AccountingForSurgical", fAccountingForSurgical, value)
        End Set
    End Property
    Dim fProductSalesMainAccountId As Integer
    Public Property ProductSalesMainAccountId() As Integer
        Get
            Return fProductSalesMainAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProductSalesMainAccountId", fProductSalesMainAccountId, value)
        End Set
    End Property
    Dim fProductSalesCashReceiptConceptId As Integer
    Public Property ProductSalesCashReceiptConceptId() As Integer
        Get
            Return fProductSalesCashReceiptConceptId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProductSalesCashReceiptConceptId", fProductSalesCashReceiptConceptId, value)
        End Set
    End Property
    Dim fProductSalesCostCenterId As Integer
    Public Property ProductSalesCostCenterId() As Integer
        Get
            Return fProductSalesCostCenterId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProductSalesCostCenterId", fProductSalesCostCenterId, value)
        End Set
    End Property
    Dim fRecoveryFeeDiscountMainAccountId As Integer
    Public Property RecoveryFeeDiscountMainAccountId() As Integer
        Get
            Return fRecoveryFeeDiscountMainAccountId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RecoveryFeeDiscountMainAccountId", fRecoveryFeeDiscountMainAccountId, value)
        End Set
    End Property
    Dim fRecoveryFeeDiscountCostCenterId As Integer
    Public Property RecoveryFeeDiscountCostCenterId() As Integer
        Get
            Return fRecoveryFeeDiscountCostCenterId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RecoveryFeeDiscountCostCenterId", fRecoveryFeeDiscountCostCenterId, value)
        End Set
    End Property
    Dim fPermissionCategories As Byte
    Public Property PermissionCategories() As Byte
        Get
            Return fPermissionCategories
        End Get
        Set(ByVal value As Byte)
            SetPropertyValue(Of Byte)("PermissionCategories", fPermissionCategories, value)
        End Set
    End Property
    Dim fCreationUser As String
    <Size(20)> _
    Public Property CreationUser() As String
        Get
            Return fCreationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CreationUser", fCreationUser, value)
        End Set
    End Property
    Dim fCreationDate As DateTime
    Public Property CreationDate() As DateTime
        Get
            Return fCreationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CreationDate", fCreationDate, value)
        End Set
    End Property
    Dim fModificationUser As String
    <Size(20)> _
    Public Property ModificationUser() As String
        Get
            Return fModificationUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ModificationUser", fModificationUser, value)
        End Set
    End Property
    Dim fModificationDate As DateTime
    Public Property ModificationDate() As DateTime
        Get
            Return fModificationDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ModificationDate", fModificationDate, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
