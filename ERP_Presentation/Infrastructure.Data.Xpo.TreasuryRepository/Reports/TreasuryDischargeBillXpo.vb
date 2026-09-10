Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Treasury.DischargeBill")> _
Public Class TreasuryDischargeBillXpo
    Inherits XPLiteObject
#Region "Members"
    Dim fId As Integer
    <Key(True)>
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fIdVoucherTransactionD As TreasuryVoucherTransactionDetailsXpo
    <Association("TreasuryDischargeBillXpoReferencesTreasuryVoucherTransactionDetailsXpo")>
    Public Property IdVoucherTransactionD() As TreasuryVoucherTransactionDetailsXpo
        Get
            Return fIdVoucherTransactionD
        End Get
        Set(ByVal value As TreasuryVoucherTransactionDetailsXpo)
            SetPropertyValue(Of TreasuryVoucherTransactionDetailsXpo)("IdVoucherTransactionD", fIdVoucherTransactionD, value)
        End Set
    End Property
    Dim fIdAccountPayable As PaymentsAccountPayableXpo
    <Association("TreasuryDischargeBillXpoReferencesPaymentsAccountPayableXpo")>
    Public Property IdAccountPayable() As PaymentsAccountPayableXpo
        Get
            Return fIdAccountPayable
        End Get
        Set(ByVal value As PaymentsAccountPayableXpo)
            SetPropertyValue(Of PaymentsAccountPayableXpo)("IdAccountPayable", fIdAccountPayable, value)
        End Set
    End Property
    Dim fIdAccountPayableShare As TreasuryAccountPayableSharesXpo
    <Association("TreasuryDischargeBillXpoReferencesTreasuryAccountPayableSharesXpo")>
    Public Property IdAccountPayableShare() As TreasuryAccountPayableSharesXpo
        Get
            Return fIdAccountPayableShare
        End Get
        Set(ByVal value As TreasuryAccountPayableSharesXpo)
            SetPropertyValue(Of TreasuryAccountPayableSharesXpo)("IdAccountPayableShare", fIdAccountPayableShare, value)
        End Set
    End Property
    Dim fAdvancedValue As Decimal
    Public Property AdvancedValue() As Decimal
        Get
            Return fAdvancedValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AdvancedValue", fAdvancedValue, value)
        End Set
    End Property
    Dim fAdvancePercent As Decimal
    Public Property AdvancePercent() As Decimal
        Get
            Return fAdvancePercent
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AdvancePercent", fAdvancePercent, value)
        End Set
    End Property
    Dim fIdPaymentConcept As TreasuryPaymentConceptsXpo
    <Association("TreasuryDischargeBillXpoReferencesTreasuryPaymentConceptsXpo")>
    Public Property IdPaymentConcept() As TreasuryPaymentConceptsXpo
        Get
            Return fIdPaymentConcept
        End Get
        Set(ByVal value As TreasuryPaymentConceptsXpo)
            SetPropertyValue(Of TreasuryPaymentConceptsXpo)("IdPaymentConcept", fIdPaymentConcept, value)
        End Set
    End Property
    Dim fBaseValueDiscount As Decimal
    Public Property BaseValueDiscount() As Decimal
        Get
            Return fBaseValueDiscount
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BaseValueDiscount", fBaseValueDiscount, value)
        End Set
    End Property
    Dim fDiscountPercent As Decimal
    Public Property DiscountPercent() As Decimal
        Get
            Return fDiscountPercent
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DiscountPercent", fDiscountPercent, value)
        End Set
    End Property

    Dim fValueInCurrencyHeader As Decimal
    <Persistent("ValueInCurrencyHeader")>
    Public Property ValueInCurrencyHeader() As Decimal
        Get
            Return fValueInCurrencyHeader
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueInCurrencyHeader", fValueInCurrencyHeader, value)
        End Set
    End Property

    Dim fTRMValue As Decimal
    <Persistent("TRMValue")>
    Public Property TRMValue() As Decimal
        Get
            Return fTRMValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TRMValue", fTRMValue, value)
        End Set
    End Property

    Dim fValueDiscountInCurrencyHeader As Decimal
    <Persistent("ValueDiscountInCurrencyHeader")>
    Public Property ValueDiscountInCurrencyHeader() As Decimal
        Get
            Return fValueDiscountInCurrencyHeader
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueDiscountInCurrencyHeader", fValueDiscountInCurrencyHeader, value)
        End Set
    End Property
#End Region

#Region "Builder"
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
#End Region

End Class
