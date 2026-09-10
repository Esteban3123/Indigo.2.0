'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.TreasuryRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 31-07-2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

<Persistent("Treasury.DischargeBill")> _
Public Class DischargeBillXpo
    Inherits XPLiteObject

#Region "Members"

    Dim fId As Integer
    <Key(True)> _
    <Persistent("Id")> _
    Public Property Id() As Integer
        Get
            Return fId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Id", fId, value)
        End Set
    End Property
    Dim fIdVoucherTransactionD As Integer
    <Persistent("IdVoucherTransactionD")> _
    Public Property IdVoucherTransactionD() As Integer
        Get
            Return fIdVoucherTransactionD
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdVoucherTransactionD", fIdVoucherTransactionD, value)
        End Set
    End Property
    Dim fIdAccountPayable As Integer
    <Persistent("IdAccountPayable")> _
    Public Property IdAccountPayable() As Integer
        Get
            Return fIdAccountPayable
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdAccountPayable", fIdAccountPayable, value)
        End Set
    End Property
    Dim fIdAccountPayableShare As AccountPayableSharesXpo
    <Association("DischargeBillReferenceAccountPayableShare")> _
    Public Property IdAccountPayableShare() As AccountPayableSharesXpo
        Get
            Return fIdAccountPayableShare
        End Get
        Set(ByVal value As AccountPayableSharesXpo)
            SetPropertyValue(Of AccountPayableSharesXpo)("IdAccountPayableShare", fIdAccountPayableShare, value)
        End Set
    End Property
    Dim fAdvancedValue As Decimal
    <Persistent("AdvancedValue")> _
    Public Property AdvancedValue() As Decimal
        Get
            Return fAdvancedValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AdvancedValue", fAdvancedValue, value)
        End Set
    End Property
    Dim fAdvancePercent As Decimal
    <Persistent("AdvancePercent")> _
    Public Property AdvancePercent() As Decimal
        Get
            Return fAdvancePercent
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("AdvancePercent", fAdvancePercent, value)
        End Set
    End Property
    Dim fIdPaymentConcept As Integer
    <Persistent("IdPaymentConcept")> _
    Public Property IdPaymentConcept() As Integer
        Get
            Return fIdPaymentConcept
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdPaymentConcept", fIdPaymentConcept, value)
        End Set
    End Property
    Dim fBaseValueDiscount As Decimal
    <Persistent("BaseValueDiscount")> _
    Public Property BaseValueDiscount() As Decimal
        Get
            Return fBaseValueDiscount
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("BaseValueDiscount", fBaseValueDiscount, value)
        End Set
    End Property
    Dim fDiscountPercent As Decimal
    <Persistent("DiscountPercent")>
    Public Property DiscountPercent() As Decimal
        Get
            Return fDiscountPercent
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DiscountPercent", fDiscountPercent, value)
        End Set
    End Property
    Dim fPaymentOrderValue As Decimal
    <Persistent("PaymentOrderValue")>
    Public Property PaymentOrderValue() As Decimal
        Get
            Return fPaymentOrderValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PaymentOrderValue", fPaymentOrderValue, value)
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

#Region "Navigations"

    <Association("DischargeBillBudget_Reference_DischargeBill", GetType(DischargeBillBudgetXpo))>
    Public ReadOnly Property DischargeBillBudgetXpo() As XPCollection(Of DischargeBillBudgetXpo)
        Get
            Return GetCollection(Of DischargeBillBudgetXpo)("DischargeBillBudgetXpo")
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