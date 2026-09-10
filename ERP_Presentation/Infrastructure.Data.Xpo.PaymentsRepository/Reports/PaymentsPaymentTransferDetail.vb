Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Payments.PaymentTransferDetail")> _
Public Class PaymentsPaymentTransferDetail
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
    Dim fPaymentTransferId As PaymentsPaymentTransfer
    <Association("PaymentsPaymentTransferDetailReferencesPaymentsPaymentTransfer")> _
    Public Property PaymentTransferId() As PaymentsPaymentTransfer
        Get
            Return fPaymentTransferId
        End Get
        Set(ByVal value As PaymentsPaymentTransfer)
            SetPropertyValue(Of PaymentsPaymentTransfer)("PaymentTransferId", fPaymentTransferId, value)
        End Set
    End Property
    Dim fAccountPayableId As PaymentsAccountPayable
    <Association("PaymentsPaymentTransferDetailReferencesPaymentsAccountPayable")> _
    Public Property AccountPayableId() As PaymentsAccountPayable
        Get
            Return fAccountPayableId
        End Get
        Set(ByVal value As PaymentsAccountPayable)
            SetPropertyValue(Of PaymentsAccountPayable)("AccountPayableId", fAccountPayableId, value)
        End Set
    End Property
    Dim fAccountPayableShareId As PaymentsAccountPayableSharesXpoP
    <Association("PaymentsPaymentTransferDetailReferencesPaymentsAccountPayableSharesXpoP")> _
    Public Property AccountPayableShareId() As PaymentsAccountPayableSharesXpoP
        Get
            Return fAccountPayableShareId
        End Get
        Set(ByVal value As PaymentsAccountPayableSharesXpoP)
            SetPropertyValue(Of PaymentsAccountPayableSharesXpoP)("AccountPayableShareId", fAccountPayableShareId, value)
        End Set
    End Property
    Dim fMainAccountId As GeneralLedgerMainAccountsXpo
    <Association("PaymentsPaymentTransferDetailReferencesGeneralLedgerMainAccountsXpo")> _
    Public Property MainAccountId() As GeneralLedgerMainAccountsXpo
        Get
            Return fMainAccountId
        End Get
        Set(ByVal value As GeneralLedgerMainAccountsXpo)
            SetPropertyValue(Of GeneralLedgerMainAccountsXpo)("MainAccountId", fMainAccountId, value)
        End Set
    End Property
    Dim fCostCenterId As PayrollCostCenterXpoP
    <Association("PaymentsPaymentTransferDetailReferencesPayrollCostCenterXpoP")> _
    Public Property CostCenterId() As PayrollCostCenterXpoP
        Get
            Return fCostCenterId
        End Get
        Set(ByVal value As PayrollCostCenterXpoP)
            SetPropertyValue(Of PayrollCostCenterXpoP)("CostCenterId", fCostCenterId, value)
        End Set
    End Property
    Dim fValue As Decimal
    Public Property Value() As Decimal
        Get
            Return fValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Value", fValue, value)
        End Set
    End Property
    Dim fRetentionConceptRTFId As Integer
    Public Property RetentionConceptRTFId() As Integer
        Get
            Return fRetentionConceptRTFId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RetentionConceptRTFId", fRetentionConceptRTFId, value)
        End Set
    End Property
    Dim fPercentageRTF As Decimal
    Public Property PercentageRTF() As Decimal
        Get
            Return fPercentageRTF
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PercentageRTF", fPercentageRTF, value)
        End Set
    End Property
    Dim fValueRTF As Decimal
    Public Property ValueRTF() As Decimal
        Get
            Return fValueRTF
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueRTF", fValueRTF, value)
        End Set
    End Property
    Dim fRetentionConceptICAId As Integer
    Public Property RetentionConceptICAId() As Integer
        Get
            Return fRetentionConceptICAId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("RetentionConceptICAId", fRetentionConceptICAId, value)
        End Set
    End Property
    Dim fValueICA As Decimal
    Public Property ValueICA() As Decimal
        Get
            Return fValueICA
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueICA", fValueICA, value)
        End Set
    End Property
    Dim fPercentageICA As Decimal
    Public Property PercentageICA() As Decimal
        Get
            Return fPercentageICA
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PercentageICA", fPercentageICA, value)
        End Set
    End Property
    Dim fValueDiscount As Decimal
    Public Property ValueDiscount() As Decimal
        Get
            Return fValueDiscount
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueDiscount", fValueDiscount, value)
        End Set
    End Property
    Dim fValueOther As Decimal
    Public Property ValueOther() As Decimal
        Get
            Return fValueOther
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueOther", fValueOther, value)
        End Set
    End Property
    Dim fValueUse As Decimal
    Public Property ValueUse() As Decimal
        Get
            Return fValueUse
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueUse", fValueUse, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Sub New()
        MyBase.New(Session.DefaultSession)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
