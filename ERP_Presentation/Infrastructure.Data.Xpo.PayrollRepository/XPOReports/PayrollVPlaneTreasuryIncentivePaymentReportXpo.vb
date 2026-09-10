Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports System.ComponentModel

<Persistent("Payroll.ViewReportIncentivePaymentPlaneTreasury")> _
Public Class PayrollVPlaneTreasuryIncentivePaymentReportXpo
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
    Dim fNit As String
    '<Indexed(Name:="IX_ThirdParty_Nit_UNI", Unique:=True)> _
    <Size(15)> _
    Public Property Nit() As String
        Get
            Return fNit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Nit", fNit, value)
        End Set
    End Property
    Dim fThirdName As String
    <Size(80)> _
    Public Property ThirdName() As String
        Get
            Return fThirdName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ThirdName", fThirdName, value)
        End Set
    End Property
    Dim fPeriodInitialDate As DateTime
    Public Property PeriodInitialDate() As DateTime
        Get
            Return fPeriodInitialDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("PeriodInitialDate", fPeriodInitialDate, value)
        End Set
    End Property
    Dim fPeriodEndDate As DateTime
    Public Property PeriodEndDate() As DateTime
        Get
            Return fPeriodEndDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("PeriodEndDate", fPeriodEndDate, value)
        End Set
    End Property
    Dim fPeriod As Char
    Public Property Period() As Char
        Get
            Return fPeriod
        End Get
        Set(ByVal value As Char)
            SetPropertyValue(Of Char)("Period", fPeriod, value)
        End Set
    End Property
    Dim fTotalPaid As Decimal
    Public Property TotalPaid() As Decimal
        Get
            Return fTotalPaid
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue(Of Decimal)("TotalPaid", fTotalPaid, value)
        End Set
    End Property
    Dim fGroupId As Integer
    Public Property GroupId() As Integer
        Get
            Return fGroupId
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("GroupId", fGroupId, value)
        End Set
    End Property
    Dim fGroupCode As String
    '<Indexed(Name:="IX_Group", Unique:=True)> _
    <Size(20)> _
    Public Property GroupCode() As String
        Get
            Return fGroupCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupCode", fGroupCode, value)
        End Set
    End Property
    Dim fGroupName As String
    <Size(150)> _
    Public Property GroupName() As String
        Get
            Return fGroupName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupName", fGroupName, value)
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
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
