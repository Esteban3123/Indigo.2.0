'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.PaymentsRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 10-07-2014
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"

Imports System
Imports DevExpress.Xpo

#End Region

''' <summary>
''' conceptos de nota usado en los servicios Xpo
''' </summary>
<Persistent("Payments.AccountPayableShares")> _
Public Class AccountPayableSharesXpo
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

    Dim fIdAccountPayable As AccountPayableXpo
    <Association("AccountPayableShareReferencesAccountPayable")> _
    Public Property IdAccountPayable() As AccountPayableXpo
        Get
            Return fIdAccountPayable
        End Get
        Set(ByVal value As AccountPayableXpo)
            SetPropertyValue(Of AccountPayableXpo)("IdAccountPayable", fIdAccountPayable, value)
        End Set
    End Property

    <PersistentAlias("IdAccountPayable.BillNumber")>
    Public ReadOnly Property NumberInvoice() As String
        Get
            Return Convert.ToString(Me.EvaluateAlias("NumberInvoice"))
        End Get
    End Property

    Dim fShare As Integer
    <Persistent("Share")> _
    Public Property Share() As Integer
        Get
            Return fShare
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Share", fShare, value)
        End Set
    End Property

    Dim fDateExpires As DateTime
    <Persistent("DateExpires")> _
    Public Property DateExpires() As DateTime
        Get
            Return fDateExpires
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("DateExpires", fDateExpires, value)
        End Set
    End Property

    Dim fInitialValue As Decimal
    <Persistent("InitialValue")> _
    Public Property InitialValue() As Decimal
        Get
            Return fInitialValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("InitialValue", fInitialValue, value)
        End Set
    End Property

    Dim fDebitValue As Decimal
    <Persistent("DebitValue")> _
    Public Property DebitValue() As Decimal
        Get
            Return fDebitValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("DebitValue", fDebitValue, value)
        End Set
    End Property

    Dim fCreditValue As Decimal
    <Persistent("CreditValue")> _
    Public Property CreditValue() As Decimal
        Get
            Return fCreditValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("CreditValue", fCreditValue, value)
        End Set
    End Property

    Dim fValueTransfers As Decimal
    <Persistent("ValueTransfers")> _
    Public Property ValueTransfers() As Decimal
        Get
            Return fValueTransfers
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("ValueTransfers", fValueTransfers, value)
        End Set
    End Property

    Dim fPaymentValue As Decimal
    <Persistent("PaymentValue")> _
    Public Property PaymentValue() As Decimal
        Get
            Return fPaymentValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("PaymentValue", fPaymentValue, value)
        End Set
    End Property

    Dim fBalance As Decimal
    <Persistent("Balance")> _
    Public Property Balance() As Decimal
        Get
            Return fBalance
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("Balance", fBalance, value)
        End Set
    End Property
    <Association("DischargeBillReferenceAccountPayableShare", GetType(DischargeBillXpo))> _
    Public ReadOnly Property DischargeBillXpo() As XPCollection(Of DischargeBillXpo)
        Get
            Return GetCollection(Of DischargeBillXpo)("DischargeBillXpo")
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
