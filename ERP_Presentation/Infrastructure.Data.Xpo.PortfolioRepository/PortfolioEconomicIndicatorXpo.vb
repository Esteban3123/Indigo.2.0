Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

<Persistent("Portfolio.EconomicIndicator")> _
Public Class PortfolioEconomicIndicatorXpo
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
    Dim fCode As String
    <Size(20)> _
    <Persistent("Code")> _
    Public Property Code() As String
        Get
            Return fCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Code", fCode, value)
        End Set
    End Property
    Dim fYear As String
    <Size(4)> _
    <Persistent("Year")> _
    Public Property Year() As String
        Get
            Return fYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Year", fYear, value)
        End Set
    End Property
    Dim fMoth As String
    <Size(2)> _
    <Persistent("Month")> _
    Public Property Moth() As String
        Get
            Return fMoth
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Month", fMoth, value)
        End Set
    End Property
    Dim fFinancialInterests As Decimal
    <Size(5.2)> _
    <Persistent("FinancialInterests")> _
    Public Property FinancialInterests() As Decimal
        Get
            Return fFinancialInterests
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("FinancialInterests", fFinancialInterests, value)
        End Set
    End Property
    Dim fLatePaymentInterest As Decimal
    <Size(5.2)> _
    <Persistent("LatePaymentInterest")> _
    Public Property LatePaymentInterest() As Decimal
        Get
            Return fLatePaymentInterest
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("LatePaymentInterest", fLatePaymentInterest, value)
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
