Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Payments.AccountPayableShares")> _
Public Class TreasuryAccountPayableSharesXpo
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
    Dim fIdAccountPayable As PaymentsAccountPayableXpo
    <Association("TreasuryAccountPayableSharesXpoReferencesPaymentsAccountPayableXpo")> _
    Public Property IdAccountPayable() As PaymentsAccountPayableXpo
        Get
            Return fIdAccountPayable
        End Get
        Set(ByVal value As PaymentsAccountPayableXpo)
            SetPropertyValue(Of PaymentsAccountPayableXpo)("IdAccountPayable", fIdAccountPayable, value)
        End Set
    End Property
    Dim fShare As Integer
    Public Property Share() As Integer
        Get
            Return fShare
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Share", fShare, value)
        End Set
    End Property
    Dim fDateExpires As DateTime
    Public Property DateExpires() As DateTime
        Get
            Return fDateExpires
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DateExpires", fDateExpires, value)
        End Set
    End Property
    Dim fInitialValue As Decimal
    Public Property InitialValue() As Decimal
        Get
            Return fInitialValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("InitialValue", fInitialValue, value)
        End Set
    End Property
    Dim fDebitValue As Decimal
    Public Property DebitValue() As Decimal
        Get
            Return fDebitValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DebitValue", fDebitValue, value)
        End Set
    End Property
    Dim fCreditValue As Decimal
    Public Property CreditValue() As Decimal
        Get
            Return fCreditValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CreditValue", fCreditValue, value)
        End Set
    End Property
    Dim fValueTransfers As Decimal
    Public Property ValueTransfers() As Decimal
        Get
            Return fValueTransfers
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueTransfers", fValueTransfers, value)
        End Set
    End Property
    Dim fPaymentValue As Decimal
    Public Property PaymentValue() As Decimal
        Get
            Return fPaymentValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PaymentValue", fPaymentValue, value)
        End Set
    End Property
    Dim fCrossingValue As Decimal
    Public Property CrossingValue() As Decimal
        Get
            Return fCrossingValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CrossingValue", fCrossingValue, value)
        End Set
    End Property
    Dim fBalance As Decimal
    Public Property Balance() As Decimal
        Get
            Return fBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Balance", fBalance, value)
        End Set
    End Property
    <Association("TreasuryDischargeBillXpoReferencesTreasuryAccountPayableSharesXpo", GetType(TreasuryDischargeBillXpo))> _
    Public ReadOnly Property TreasuryDischargeBillXpo() As XPCollection(Of TreasuryDischargeBillXpo)
        Get
            Return GetCollection(Of TreasuryDischargeBillXpo)("TreasuryDischargeBillXpo")
        End Get
    End Property
    <Association("TreasurySchedulePaymentDetailXpoReferencesTreasuryAccountPayableSharesXpo", GetType(TreasurySchedulePaymentDetailXpo))> _
    Public ReadOnly Property TreasurySchedulePaymentDetailXpo() As XPCollection(Of TreasurySchedulePaymentDetailXpo)
        Get
            Return GetCollection(Of TreasurySchedulePaymentDetailXpo)("TreasurySchedulePaymentDetailXpo")
        End Get
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
