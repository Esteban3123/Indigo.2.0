Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Treasury.DischargeBill")> _
Public Class TreasuryDischargeBill
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
    Dim fIdVoucherTransactionD As TreasuryVoucherTransactionDetails
    <Association("TreasuryDischargeBillReferencesTreasuryVoucherTransactionDetails")> _
    Public Property IdVoucherTransactionD() As TreasuryVoucherTransactionDetails
        Get
            Return fIdVoucherTransactionD
        End Get
        Set(ByVal value As TreasuryVoucherTransactionDetails)
            SetPropertyValue(Of TreasuryVoucherTransactionDetails)("IdVoucherTransactionD", fIdVoucherTransactionD, value)
        End Set
    End Property
    Dim fIdAccountPayable As PaymentsAccountPayable
    <Association("TreasuryDischargeBillReferencesPaymentsAccountPayable")> _
    Public Property IdAccountPayable() As PaymentsAccountPayable
        Get
            Return fIdAccountPayable
        End Get
        Set(ByVal value As PaymentsAccountPayable)
            SetPropertyValue(Of PaymentsAccountPayable)("IdAccountPayable", fIdAccountPayable, value)
        End Set
    End Property
    Dim fIdAccountPayableShare As Integer
    Public Property IdAccountPayableShare() As Integer
        Get
            Return fIdAccountPayableShare
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdAccountPayableShare", fIdAccountPayableShare, value)
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
    Dim fIdPaymentConcept As Integer
    Public Property IdPaymentConcept() As Integer
        Get
            Return fIdPaymentConcept
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdPaymentConcept", fIdPaymentConcept, value)
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
