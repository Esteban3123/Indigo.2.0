Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

#Region "Structure"

Public Structure BalanceKey

    <Persistent("IdAccount")> _
    Public Property IdAccount As Integer

    <Persistent("YearBalance")> _
    Public Property YearBalance As Integer

    <Persistent("MonthBalance")> _
    Public Property MonthBalance As Integer

End Structure

#End Region

<Persistent("GeneralLedger.VBalanceMainAccount")> _
Public Class GeneralLedgerVBalanceMainAccountXpo
    Inherits XPLiteObject

    <Key(), Persistent()> _
    Public Property Key As BalanceKey

    Dim fYearBalance As Integer
    Public Property YearBalance() As Integer
        Get
            Return fYearBalance
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("YearBalance", fYearBalance, value)
        End Set
    End Property
    Dim fMonthBalance As Integer
    Public Property MonthBalance() As Integer
        Get
            Return fMonthBalance
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MonthBalance", fMonthBalance, value)
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
    Dim fNumberAccount As String
    <Size(50)> _
    Public Property NumberAccount() As String
        Get
            Return fNumberAccount
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NumberAccount", fNumberAccount, value)
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
    Dim fLegalBookId As Integer
    Public Property LegalBookId() As Integer
        Get
            Return fLegalBookId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("LegalBookId", fLegalBookId, value)
        End Set
    End Property

    Dim fIdAccount As Integer
    Public Property IdAccount() As Integer
        Get
            Return fIdAccount
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("IdAccount", fIdAccount, value)
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
