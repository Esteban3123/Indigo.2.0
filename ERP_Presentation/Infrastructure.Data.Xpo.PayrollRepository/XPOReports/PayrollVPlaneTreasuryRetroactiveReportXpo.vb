Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.ViewReportPlaneTreasuryRetroactive")> _
Public Class PayrollVPlaneTreasuryRetroactiveReportXpo
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
    Dim fBankAccountNumber As String
    <Size(20)> _
    Public Property BankAccountNumber() As String
        Get
            Return fBankAccountNumber
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BankAccountNumber", fBankAccountNumber, value)
        End Set
    End Property
    Dim fBankName As String
    <Size(320)> _
    Public Property BankName() As String
        Get
            Return fBankName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BankName", fBankName, value)
        End Set
    End Property
    Dim fNit As String
    <Size(20)> _
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
        End Set
    End Property
    Dim fThirdPartyName As String
    <Size(300)> _
    Public Property ThirdPartyName() As String
        Get
            Return fThirdPartyName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdPartyName", fThirdPartyName, value)
        End Set
    End Property
    Dim fTotalRetroactiveValue As Decimal
    Public Property TotalRetroactiveValue() As Decimal
        Get
            Return fTotalRetroactiveValue
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalRetroactiveValue", fTotalRetroactiveValue, value)
        End Set
    End Property
    Dim fInitialDateRetroactive As DateTime
    Public Property InitialDateRetroactive() As DateTime
        Get
            Return fInitialDateRetroactive
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("InitialDateRetroactive", fInitialDateRetroactive, value)
        End Set
    End Property
    Dim fCodeGroup As String
    <Size(20)> _
    Public Property CodeGroup() As String
        Get
            Return fCodeGroup
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CodeGroup", fCodeGroup, value)
        End Set
    End Property
    Dim fNameGroup As String
    <Size(150)> _
    Public Property NameGroup() As String
        Get
            Return fNameGroup
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("NameGroup", fNameGroup, value)
        End Set
    End Property
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
