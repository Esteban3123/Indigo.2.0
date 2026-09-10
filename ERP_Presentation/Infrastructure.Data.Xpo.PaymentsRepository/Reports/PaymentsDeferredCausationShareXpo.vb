Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payments.DeferredCausationShare")> _
Public Class PaymentsDeferredCausationShareXpo
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
    Dim fDeferredCausationId As PaymentsDeferredCausationXpo
    <Association("PaymentsDeferredCausationShareXpoReferencesPayments_DeferredCausation")> _
    Public Property DeferredCausationId() As PaymentsDeferredCausationXpo
        Get
            Return fDeferredCausationId
        End Get
        Set(ByVal value As PaymentsDeferredCausationXpo)
            SetPropertyValue(Of PaymentsDeferredCausationXpo)("DeferredCausationId", fDeferredCausationId, value)
        End Set
    End Property
    Dim fPaymentMonth As Integer
    Public Property PaymentMonth() As Integer
        Get
            Return fPaymentMonth
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PaymentMonth", fPaymentMonth, value)
        End Set
    End Property
    Dim fPaymentYear As Integer
    Public Property PaymentYear() As Integer
        Get
            Return fPaymentYear
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PaymentYear", fPaymentYear, value)
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
    Dim fAmortized As Boolean
    Public Property Amortized() As Boolean
        Get
            Return fAmortized
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("Amortized", fAmortized, value)
        End Set
    End Property

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

End Class
